using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllureViewerPortable
{
    public partial class MainForm : Form
    {
        private readonly string _baseDir;
        private string? _sessionDir;
        private string? _webRoot;
        private ServerHost? _server;
        private readonly StringBuilder _log = new();
        private string _logFilePath;
        private CancellationTokenSource? _cts;

        public MainForm(string baseDir)
        {
            InitializeComponent();
            _baseDir = baseDir;
            _logFilePath = Path.Combine(_baseDir, "logs", $"{DateTime.Now:yyyyMMdd_HHmmss}.log");

            // Firma del desarrollador (StatusStrip)
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                var infoVersion = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                         ?? asm.GetName().Version?.ToString();
                // Separa la cadena en el signo '+' y toma la primera parte.
                var version = infoVersion.Split('+')[0]; 
                var exePath = asm.Location;
                var buildDate = File.Exists(exePath) ? File.GetLastWriteTime(exePath) : DateTime.Now;

                lblFirma.Text = $"Desarrollo Jorge Luis Bergandi — v{version}     — Fecha de ultima actualizacion: {buildDate:yyyy-MM-dd}";
                lblFirma.Click += (s, e) =>
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://www.linkedin.com/in/jorge-luis-bergandi/",
                            UseShellExecute = true
                        });
                    }
                    catch { }
                };
            }
            catch { }
        }

        private void Log(string msg)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {msg}";
            _log.AppendLine(line);
            lblEstado.Text = $"Estado: {msg}";
            try { File.AppendAllText(_logFilePath, line + Environment.NewLine, Encoding.UTF8); } catch { }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Archivos ZIP (*.zip)|*.zip",
                Title = "Seleccionar reporte Allure (.zip)",
                CheckFileExists = true,
                Multiselect = false
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                txtZip.Text = ofd.FileName;
                Log($"Archivo seleccionado: {ofd.FileName}");
            }
        }

        private async void btnVisualizar_Click(object sender, EventArgs e)
        {
            btnVisualizar.Enabled = false;
            btnGuardarLog.Enabled = false;
            try
            {
                var zipPath = txtZip.Text?.Trim();
                if (string.IsNullOrEmpty(zipPath) || !File.Exists(zipPath))
                    throw new InvalidOperationException("Seleccione un archivo .zip válido.");

                if (!zipPath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("El archivo debe tener extensión .zip.");

                Log("Validando .zip...");
                using (var fs = File.OpenRead(zipPath))
                using (var ar = new ZipArchive(fs, ZipArchiveMode.Read, leaveOpen: false))
                {
                    if (ar.Entries.Count == 0)
                        throw new InvalidOperationException("El archivo .zip está vacío.");
                }

                _sessionDir = Path.Combine(_baseDir, "session_" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(_sessionDir);
                Log($"Descomprimiendo en: {_sessionDir}");

                await Task.Run(() => ZipFile.ExtractToDirectory(zipPath, _sessionDir!, overwriteFiles: true));

                // index.html en raíz o en la primera carpeta
                var rootIndex = Path.Combine(_sessionDir!, "index.html");
                if (File.Exists(rootIndex))
                {
                    _webRoot = _sessionDir!;
                }
                else
                {
                    var firstFolder = Directory.GetDirectories(_sessionDir!).FirstOrDefault();
                    if (!string.IsNullOrEmpty(firstFolder))
                    {
                        var nestedIndex = Path.Combine(firstFolder, "index.html");
                        if (File.Exists(nestedIndex))
                        {
                            _webRoot = firstFolder;
                        }
                    }
                }

                if (string.IsNullOrEmpty(_webRoot))
                    throw new InvalidOperationException("El .zip no contiene un reporte válido: no se encontró 'index.html' en la raíz ni en la primera carpeta.");

                Log("Iniciando servidor local...");
                _server = new ServerHost(_webRoot);
                _cts = new CancellationTokenSource();
                await _server.StartAsync(_cts.Token);

                Log($"Servidor en {_server.BaseAddress}. Abriendo navegador...");
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = _server.BaseAddress,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Log($"No se pudo abrir el navegador automáticamente: {ex.Message}");
                    MessageBox.Show(this, $"Abrí manualmente: {_server.BaseAddress}", "Abrir navegador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Log("Reporte en ejecución.");
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.ToString());
                btnGuardarLog.Enabled = true;
                MessageBox.Show(this,
                    "No se pudo mostrar el reporte.\n\nDetalle:\n" + ex.Message +
                    "\n\nSugerencias:\n- Verificá que el ZIP tenga 'index.html' en la raíz o dentro de la primera carpeta.\n- Probá descomprimir el ZIP y revisá que no esté vacío o corrupto.\n- Intentá con otro archivo.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnVisualizar.Enabled = true;
            }
        }

        private async Task CleanupAsync()
        {
            Log("Deteniendo servidor y limpiando archivos temporales...");
            try { _cts?.Cancel(); } catch { }

            if (_server != null)
            {
                try { await _server.DisposeAsync(); } catch { }
                _server = null;
            }

            if (!string.IsNullOrEmpty(_sessionDir) && Directory.Exists(_sessionDir))
            {
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try { Directory.Delete(_sessionDir, recursive: true); break; }
                        catch { await Task.Delay(300); }
                    }
                }
                catch { }
            }

            Log("Limpieza finalizada.");
        }

        private async void btnCerrar_Click(object sender, EventArgs e)
        {
            await CleanupAsync();
            Close();
        }

        private async void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            await CleanupAsync();
        }

        private void btnGuardarLog_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo de texto (*.txt)|*.txt",
                FileName = "AllureViewer_log.txt"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(sfd.FileName, _log.ToString(), Encoding.UTF8);
                    MessageBox.Show(this, "Log guardado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "No se pudo guardar el log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

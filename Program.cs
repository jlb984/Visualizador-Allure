using System;
using System.IO;
using System.Windows.Forms;

namespace AllureViewerPortable
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var baseDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AllureViewer");

            Directory.CreateDirectory(baseDir);
            Directory.CreateDirectory(Path.Combine(baseDir, "logs"));

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(baseDir));
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace AllureViewerPortable
{
    public sealed class ServerHost : IAsyncDisposable
    {
        private IHost? _host;
        public int Port { get; private set; }
        public string BaseAddress { get; private set; } = "";
        private readonly string _webRoot;

        public ServerHost(string webRoot)
        {
            _webRoot = webRoot;
        }

        private static int GetFreePort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public async Task StartAsync(CancellationToken ct = default)
        {
            const int maxAttempts = 5;
            Exception? lastEx = null;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                try
                {
                    Port = GetFreePort();
                    BaseAddress = $"http://127.0.0.1:{Port}";

                    var builder = WebApplication.CreateBuilder();
                    builder.WebHost.UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Loopback, Port);
                    });

                    var app = builder.Build();

                    var provider = new PhysicalFileProvider(_webRoot);

                    app.UseDefaultFiles(new DefaultFilesOptions
                    {
                        FileProvider = provider
                    });

                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = provider,
                        ServeUnknownFileTypes = true
                    });

                    await app.StartAsync(ct);
                    _host = app;
                    return;
                }
                catch (Exception ex)
                {
                    lastEx = ex;
                    await Task.Delay(120);
                }
            }

            throw new InvalidOperationException("No se pudo iniciar el servidor local en un puerto libre.", lastEx);
        }

        public async ValueTask DisposeAsync()
        {
            if (_host != null)
            {
                try { await _host.StopAsync(TimeSpan.FromSeconds(2)); }
                catch { }
                finally { _host.Dispose(); }
            }
        }
    }
}
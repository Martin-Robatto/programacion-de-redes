using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = Task.Run(() => CreateHostBuilder(args).Build().Run());
                ServerHandler serverHandler = new ServerHandler();
                serverHandler.Run();
                task.Dispose();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        ISettingsManager settingsManager = new SettingsManager();
                        var grpcPort = int.Parse(settingsManager.ReadSetting(ServerConfig.GRPCPortConfigKey));
                        // Setup a HTTP/2 endpoint without TLS.
                        options.ListenLocalhost(grpcPort, o => o.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
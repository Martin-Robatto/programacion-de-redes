using System;
using System.Threading.Tasks;
using GrpcServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace ConsoleServer
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            try
            {
                ServerHandler serverHandler = new ServerHandler();
                serverHandler.Run();
                CreateHostBuilder(args).Build().Run();
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
                        // Setup a HTTP/2 endpoint without TLS.
                        options.ListenLocalhost(9000, o => o.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
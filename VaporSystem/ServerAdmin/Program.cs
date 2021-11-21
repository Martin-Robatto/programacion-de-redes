using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ServerAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        ISettingsManager settingsManager = new SettingsManager();
                        var port = int.Parse(settingsManager.ReadSetting(ServerConfig.ServerPortConfigKey));
                        // Setup a HTTP/2 endpoint without TLS.
                        options.ListenLocalhost(port);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
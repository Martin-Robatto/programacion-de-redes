using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogsServer.Logs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogReceiver.Instance.Connect();
            LogReceiver.Instance.ReceiveLogs();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
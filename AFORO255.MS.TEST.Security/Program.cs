using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MS.AFORO255.Cross.Metrics.Metrics;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace AFORO255.MS.TEST.Security
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((host, builder) =>
                    {
                        var env = host.HostingEnvironment;
                        builder.AddConfigServer(env.EnvironmentName);

                        ///////Metricas-Prometheus
                        webBuilder.UseAppMetrics();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using TradeCube_Reports.Helpers;

namespace TradeCube_Reports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(o =>
                        {
                            var port = EnvironmentVariableHelper.GetIntEnvironmentVariable("TRADECUBE_REPORTS_HTTPS_PORT");
                            var certificateInfo = X509Helper.CertificateInfo("TRADECUBE_REPORTS_CERT_NAME", "TRADECUBE_REPORTS_CERT_PASSWORD");

                            if (X509Helper.IsValidHttpsConfig(port, certificateInfo))
                            {
                                o.ListenAnyIP(port ?? 0, options => { options.UseHttps(certificateInfo.name, certificateInfo.password); });
                            }
                        })
                        .UseStartup<Startup>()
                        .ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.SetMinimumLevel(LogLevel.Trace);
                        })
                        .UseNLog();
                });
    }
}

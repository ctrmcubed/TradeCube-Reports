using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using TradeCube_Reports.Helpers;

namespace TradeCube_Reports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel(o =>
                {
                    var port = EnvironmentVariableHelper.GetIntEnvironmentVariable("TRADECUBE_REPORTS_HTTPS_PORT");
                    var certificateInfo = X509Helper.CertificateInfo("TRADECUBE_REPORTS_CERT_NAME", "TRADECUBE_REPORTS_CERT_PASSWORD");

                    if (X509Helper.IsValidHttpsConfig(port, certificateInfo))
                    {
                        o.ListenAnyIP(port ?? 0, options =>
                          {
                              options.UseHttps(certificateInfo.name, certificateInfo.password);
                          });
                    }
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .Build();
    }
}

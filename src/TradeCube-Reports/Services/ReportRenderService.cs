using jsreport.Local;
using jsreport.Types;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TradeCube_Reports.Services
{
    public class ReportRenderService : IReportRenderService
    {
        public async Task<Report> Render<T>(string template, T content)
        {
            var rs = new LocalReporting()
                .UseBinary(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                    jsreport.Binary.JsReportBinary.GetBinary() :
                    jsreport.Binary.Linux.JsReportBinary.GetBinary())
                .AsUtility()
                .Create();

            var report = await rs.RenderAsync(new RenderRequest
            {
                Template = new Template
                {
                    Recipe = Recipe.Html,
                    Engine = Engine.Handlebars,
                    Content = template
                },
                Data = new
                {
                    trades = content
                }
            });

            return report;
        }
    }
}

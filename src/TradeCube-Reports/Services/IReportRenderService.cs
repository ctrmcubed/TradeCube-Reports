using jsreport.Types;
using System.Threading.Tasks;

namespace TradeCube_Reports.Services
{
    public interface IReportRenderService
    {
        Task<Report> Render<T>(string template, T content);
    }
}
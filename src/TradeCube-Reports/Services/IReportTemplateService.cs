using System.Threading.Tasks;
using TradeCube_Reports.Messages;
using TradeCube_Reports.Models;

namespace TradeCube_Reports.Services
{
    public interface IReportTemplateService
    {
        Task<ApiResponseWrapper<ReportTemplate>> ReportTemplate(string templateType);
    }
}
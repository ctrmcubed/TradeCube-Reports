using System.Threading.Tasks;
using TradeCube_Reports.DataObjects;
using TradeCube_Reports.Models;
using TradeCube_Reports.ReportParameters;

namespace TradeCube_Reports.Services
{
    public interface IConfirmationService
    {
        Task<ApiResponseWrapper<ConfirmationReport>> CreateReport(ConfirmationReportParameters confirmationReportParameters);
    }
}
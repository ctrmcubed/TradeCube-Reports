using System.Threading.Tasks;
using TradeCube_Reports.Messages;
using TradeCube_Reports.ReportParameters;

namespace TradeCube_Reports.Services
{
    public interface IConfirmationReportService
    {
        Task<ApiResponseWrapper<WebServiceResponse>> CreateReport(ConfirmationReportParameters confirmationReportParameters);
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TradeCube_Reports.Constants;
using TradeCube_Reports.Messages;
using TradeCube_Reports.Models;
using TradeCube_Reports.ReportParameters;
using TradeCube_Reports.Services;

namespace TradeCube_Reports.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ConfirmationController : ControllerBase
    {
        private readonly IConfirmationReportService confirmationReportService;
        private readonly ILogger<ConfirmationController> logger;

        public ConfirmationController(IConfirmationReportService confirmationReportService, ILogger<ConfirmationController> logger)
        {
            this.confirmationReportService = confirmationReportService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Confirmation([FromHeader] string apiJwtToken, [FromBody] ConfirmationReportRequest confirmationReportRequest)
        {
            try
            {
                var confirmationReportParameters = new ConfirmationReportParametersBase
                {
                    ApiJwtToken = apiJwtToken,
                    Template = confirmationReportRequest.Template,
                    TradeReferences = confirmationReportRequest.TradeReferences
                };

                var confirmationReport = await confirmationReportService.CreateReport(confirmationReportParameters);

                return confirmationReport.Status == ApiConstants.SuccessResult
                    ? (IActionResult)Ok(confirmationReport)
                    : BadRequest(confirmationReport);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return BadRequest(new ApiResponseWrapper<ConfirmationReport> { Message = e.Message, Status = ApiConstants.FailedResult });
            }
        }
    }
}
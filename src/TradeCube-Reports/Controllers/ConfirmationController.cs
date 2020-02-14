using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TradeCube_Reports.Constants;
using TradeCube_Reports.DataObjects;
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
    public class ConfirmationController : ControllerBase
    {
        private readonly IConfirmationService confirmationService;
        private readonly ILogger<ConfirmationController> logger;

        public ConfirmationController(IConfirmationService confirmationService, ILogger<ConfirmationController> logger)
        {
            this.confirmationService = confirmationService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromHeader] string apiKey, [FromBody] ConfirmationReportRequest confirmationReportRequest)
        {
            try
            {
                var confirmationReportParameters = new ConfirmationReportParameters
                {
                    ApiKey = apiKey,
                    TradeReferences = confirmationReportRequest.TradeReferences
                };

                var confirmationReport = await confirmationService.CreateReport(confirmationReportParameters);

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
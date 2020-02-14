using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeCube_Reports.Constants;
using TradeCube_Reports.DataObjects;
using TradeCube_Reports.Messages;
using TradeCube_Reports.Models;
using TradeCube_Reports.ReportParameters;

namespace TradeCube_Reports.Services
{
    public class ConfirmationService : IConfirmationService
    {
        private readonly ITradeService tradeService;

        public ConfirmationService(ITradeService tradeService)
        {
            this.tradeService = tradeService;
        }

        public async Task<ApiResponseWrapper<ConfirmationReport>> CreateReport(ConfirmationReportParameters confirmationReportParameters)
        {
            static ApiResponseWrapper<ConfirmationReport> OkResponse(ApiResponseWrapper<IEnumerable<TradeDataObject>> trades)
            {
                var confirmationReport = new ConfirmationReport
                {
                    Trades = trades.Data
                };

                return new ApiResponseWrapper<ConfirmationReport>(ApiConstants.SuccessResult, confirmationReport)
                {
                    RecordCount = 1
                };
            }

            try
            {
                var apiKey = confirmationReportParameters.ApiKey;
                var request = new TradeRequest { TradeReferences = confirmationReportParameters.TradeReferences };

                var trades = await tradeService.Trades(apiKey, request);

                return trades.Status == ApiConstants.SuccessResult
                    ? OkResponse(trades)
                    : new ApiResponseWrapper<ConfirmationReport> { Message = trades.Message, Status = trades.Status };
            }
            catch (Exception e)
            {
                return new ApiResponseWrapper<ConfirmationReport> { Message = e.Message, Status = ApiConstants.FailedResult };
            }
        }
    }
}

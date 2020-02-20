﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeCube_Reports.Constants;
using TradeCube_Reports.DataObjects;
using TradeCube_Reports.Messages;
using TradeCube_Reports.ReportParameters;

namespace TradeCube_Reports.Services
{
    public class ConfirmationReportReportService : IConfirmationReportService
    {
        private readonly ITradeService tradeService;
        private readonly IReportTemplateService reportTemplateService;
        private readonly IReportRenderService reportRenderService;
        private readonly ICountryLookupService countryLookupService;

        public ConfirmationReportReportService(ITradeService tradeService, ICountryLookupService countryLookupService, IReportTemplateService reportTemplateService, IReportRenderService reportRenderService)
        {
            this.tradeService = tradeService;
            this.countryLookupService = countryLookupService;
            this.reportTemplateService = reportTemplateService;
            this.reportRenderService = reportRenderService;
        }

        public async Task<ApiResponseWrapper<WebServiceResponse>> CreateReport(ConfirmationReportParameters confirmationReportParameters)
        {
            try
            {
                var apiJwtToken = confirmationReportParameters.ApiJwtToken;
                var request = new TradeRequest { TradeReferences = confirmationReportParameters.TradeReferences };
                var trades = await tradeService.Trades(apiJwtToken, request);
                var enrichedTrades = await EnrichTradesWithCountries(trades.Data, confirmationReportParameters);
                var template = await reportTemplateService.ReportTemplate(confirmationReportParameters.Template);
                var tradeDataObjects = enrichedTrades.ToList();
                var report = await reportRenderService.Render(template?.Data?.Html, tradeDataObjects);
                var reader = new StreamReader(report.Content);
                var data = reader.ReadToEnd();

                return new ApiResponseWrapper<WebServiceResponse> { Status = ApiConstants.SuccessResult, Data = new WebServiceResponse { Data = data } };
            }
            catch (Exception e)
            {
                return new ApiResponseWrapper<WebServiceResponse> { Status = ApiConstants.FailedResult, Message = e.Message };
            }
        }

        async Task<IEnumerable<TradeDataObject>> EnrichTradesWithCountries(IEnumerable<TradeDataObject> trades, ReportParametersBase confirmationReportParametersBase)
        {
            await countryLookupService.Load(confirmationReportParametersBase.ApiJwtToken);

            return SetCountryLongName(trades);
        }

        private IEnumerable<TradeDataObject> SetCountryLongName(IEnumerable<TradeDataObject> trades)
        {
            foreach (var trade in trades)
            {
                // Mutate trades by setting the country property to the country long name 

                if (!string.IsNullOrEmpty(trade?.Buyer?.PrimaryConfirmationContact?.PrimaryAddress?.Country))
                {
                    var buyerCountry = countryLookupService.Lookup(trade.Buyer?.PrimaryConfirmationContact?.PrimaryAddress?.Country);

                    trade.Buyer.PrimaryConfirmationContact.PrimaryAddress.Country = buyerCountry == null
                        ? trade.Buyer?.PrimaryConfirmationContact?.PrimaryAddress?.Country
                        : buyerCountry.CountryLongName;
                }

                if (!string.IsNullOrEmpty(trade?.Seller?.PrimaryConfirmationContact?.PrimaryAddress?.Country))
                {
                    var sellerCountry = countryLookupService.Lookup(trade.Seller?.PrimaryConfirmationContact?.PrimaryAddress?.Country);

                    trade.Seller.PrimaryConfirmationContact.PrimaryAddress.Country = sellerCountry == null
                        ? trade.Seller.PrimaryConfirmationContact.PrimaryAddress.Country
                        : sellerCountry.CountryLongName;
                }

                yield return trade;
            }
        }
    }
}

﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TradeCube_Reports.Configuration;
using TradeCube_Reports.DataObjects;
using TradeCube_Reports.Messages;

namespace TradeCube_Reports.Services
{
    public class TradeService : TradeCubeApiService, ITradeService
    {
        private readonly ILogger<TradeCubeApiService> logger;

        public TradeService(IHttpClientFactory httpClientFactory, ITradeCubeConfiguration tradeCubeConfiguration,
            ILogger<TradeCubeApiService> logger) : base(httpClientFactory, tradeCubeConfiguration, logger)
        {
            this.logger = logger;
        }

        public async Task<ApiResponseWrapper<IEnumerable<TradeDataObject>>> Trades(string apiJwtToken, TradeRequest tradeRequest)
        {
            try
            {
                var query = new JObject
                {
                    new JProperty("TradeReference", new JObject(new JProperty("$in", new JArray(tradeRequest.TradeReferences))))
                };

                return await Post<ApiResponseWrapper<IEnumerable<TradeDataObject>>>(apiJwtToken, "Trade/query", query);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return new ApiResponseWrapper<IEnumerable<TradeDataObject>> { Message = e.Message, Status = HttpStatusCode.BadRequest.ToString() };
            }
        }
    }
}
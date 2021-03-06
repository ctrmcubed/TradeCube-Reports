﻿using Microsoft.Extensions.Logging;
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
    public class CountryService : TradeCubeApiService, ICountryService
    {
        private readonly ILogger<TradeCubeApiService> logger;

        public CountryService(IHttpClientFactory httpClientFactory, ITradeCubeConfiguration tradeCubeConfiguration,
            ILogger<TradeCubeApiService> logger) : base(httpClientFactory, tradeCubeConfiguration, logger)
        {
            this.logger = logger;
        }

        public async Task<ApiResponseWrapper<IEnumerable<CountryDataObject>>> Countries(string apiJwtToken)
        {
            try
            {
                return await Get<ApiResponseWrapper<IEnumerable<CountryDataObject>>>(apiJwtToken, "Country");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return new ApiResponseWrapper<IEnumerable<CountryDataObject>> { Message = e.Message, Status = HttpStatusCode.BadRequest.ToString() };
            }
        }
    }
}
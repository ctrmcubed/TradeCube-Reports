using System.Collections.Generic;
using System.Threading.Tasks;
using TradeCube_Reports.DataObjects;
using TradeCube_Reports.Messages;

namespace TradeCube_Reports.Services
{
    public interface ICountryService
    {
        Task<ApiResponseWrapper<IEnumerable<CountryDataObject>>> Countries(string apiJwtToken);
    }
}
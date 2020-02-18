using System.Threading.Tasks;
using TradeCube_Reports.DataObjects;

namespace TradeCube_Reports.Services
{
    public interface ICountryLookupService
    {
        Task Load(string apiJwtToken);
        CountryDataObject Lookup(string key);
    }
}
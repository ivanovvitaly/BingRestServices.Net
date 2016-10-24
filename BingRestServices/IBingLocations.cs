using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Locations;

namespace BingRestServices
{
    public interface IBingLocations
    {
        Task<Response> FindLocationAsync(FindLocationParameters parameters);
    }
}
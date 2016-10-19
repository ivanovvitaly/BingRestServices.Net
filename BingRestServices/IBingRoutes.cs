using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Routes;

namespace BingRestServices
{
    public interface IBingRoutes
    {
        Task<Response> CalculateRoutesAsync(CalculateRoutesParameters parameters);
    }
}
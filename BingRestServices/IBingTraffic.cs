using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Traffic;

namespace BingRestServices
{
    public interface IBingTraffic
    {
        Task<Response> GetTrafficIncidents(TrafficIncidentsParameters parameters);
    }
}
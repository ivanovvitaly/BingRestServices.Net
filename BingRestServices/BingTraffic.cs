using System;
using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Extensions;
using BingRestServices.Services;
using BingRestServices.Traffic;

using RestSharp;

namespace BingRestServices
{
    public class BingTraffic : BingService, IBingTraffic
    {
        public Task<Response> GetTrafficIncidents(TrafficIncidentsParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(
                    "parameters",
                    "Provide valid parameters for Traffic Incidents request.");
            }

            var request = new RestRequest("Traffic/Incidents/{MapArea}/", Method.GET);

            AddQueryParameters(parameters, request);

            var response = ExecuteAsync<Response>(request);

            return response;
        }

        private static void AddQueryParameters(TrafficIncidentsParameters parameters, RestRequest request)
        {
            if (parameters.MapArea == null)
            {
                throw new ArgumentNullException(
                    "MapArea",
                    "The area to search for traffic incident information is required for Traffic Incidents request.");
            }

            request.AddUrlSegment("MapArea", parameters.MapArea.ToString());

            if (parameters.IncludeLocationCodes != null)
            {
                request.AddQueryParameter("includeLocationCodes", parameters.IncludeLocationCodes.Value.ToString());
            }

            if (parameters.Severity != null)
            {
                request.AddQueryParameter("s", parameters.Severity.ToCSVString());
            }

            if (parameters.TrafficIncidentTypes != null)
            {
                request.AddQueryParameter("t", parameters.TrafficIncidentTypes.ToCSVString());
            }
        }
    }
}
using System;

using RestSharp;

namespace BingRestServices.Locations
{
    public class LocationByQueryParametersBuilder : ILocationParametersBuilder, ILocationParametersBuilder<FindLocationByQueryParameters>
    {
        public void Build(FindLocationParameters parameters, IRestRequest request)
        {
            if (parameters is FindLocationByQueryParameters)
            {
                Build(parameters as FindLocationByQueryParameters, request);
            }
        }

        public void Build(FindLocationByQueryParameters parameters, IRestRequest request)
        {
            if (parameters.Query == null)
            {
                throw new ArgumentNullException("Query", "Location information is required for Find Location By Query request.");   
            }

            request.AddQueryParameter("q", parameters.Query.GetFormattedString());
        }
    }
}
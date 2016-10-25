using System;

using BingRestServices.Extensions;

using RestSharp;

namespace BingRestServices.Locations
{
    public class LocationByPointParametersBuilder : ILocationParametersBuilder, ILocationParametersBuilder<FindLocationByPointParameters>
    {
        public void Build(FindLocationParameters parameters, IRestRequest request)
        {
            if (parameters is FindLocationByPointParameters)
            {
                Build(parameters as FindLocationByPointParameters, request);
            }
        }

        public void Build(FindLocationByPointParameters parameters, IRestRequest request)
        {
            if (parameters.Point == null)
            {
                throw new ArgumentNullException("Point", "The coordinates of the location is required for Find Location By Point request.");
            }
            
            request.AddUrlSegment("Point", parameters.Point.ToString());

            if (parameters.IncludeEntityTypes != null)
            {
                request.AddQueryParameter("includeEntityTypes", parameters.IncludeEntityTypes.ToCSVString());
            }
        }
    }
}
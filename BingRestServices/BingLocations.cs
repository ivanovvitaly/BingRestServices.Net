using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BingRestServices.Configuration;
using BingRestServices.DataContracts;
using BingRestServices.Locations;
using BingRestServices.Services;

using RestSharp;

namespace BingRestServices
{
    public class BingLocations : BingService, IBingLocations
    {
        private static readonly List<ILocationParametersBuilder> ParameterBuilders = new List<ILocationParametersBuilder>();
        private static readonly Dictionary<Type, string> ResourceMap = new Dictionary<Type, string>();

        static BingLocations()
        {
            ParameterBuilders.Add(new LocationByAddressParametersBuilder());
            ParameterBuilders.Add(new LocationByPointParametersBuilder());

            ResourceMap[typeof(FindLocationByAddressParameters)] = "Locations";
            ResourceMap[typeof(FindLocationByPointParameters)] = "Locations/{Point}";
        }

        public BingLocations()
        {
        }

        public BingLocations(BingConfiguration configuration)
            : base(configuration)
        {
        }

        public Task<Response> FindLocationAsync(FindLocationParameters parameters)
        {
            var request = new RestRequest(ResourceMap[parameters.GetType()], Method.GET);
            AddOptionalQueryParameters(parameters, request);

            foreach (var parametersBuilder in ParameterBuilders)
            {
                parametersBuilder.Build(parameters, request);
            }

            var response = ExecuteAsync<Response>(request);

            return response;
        }

        private void AddOptionalQueryParameters(FindLocationParameters parameters, IRestRequest request)
        {
            if (parameters.MaxResults != null)
            {
                request.AddQueryParameter("maxRes", parameters.MaxResults.Key);
            }
            
            if (parameters.IncludeNeighborhood != null)
            {
                request.AddQueryParameter("inclnb", parameters.IncludeNeighborhood.Key);
            }
        }
    }
}
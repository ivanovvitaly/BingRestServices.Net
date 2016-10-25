using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BingRestServices.Configuration;
using BingRestServices.DataContracts;
using BingRestServices.Extensions;
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
            ParameterBuilders.Add(new LocationByQueryParametersBuilder());

            ResourceMap[typeof(FindLocationByAddressParameters)] = "Locations";
            ResourceMap[typeof(FindLocationByPointParameters)] = "Locations/{Point}";
            ResourceMap[typeof(FindLocationByQueryParameters)] = "Locations";
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
            if (!ResourceMap.ContainsKey(parameters.GetType()))
            {
                throw new ArgumentException(string.Format("Resource URI was not found for the given type '{0}'", parameters.GetType()));
            }

            var request = new RestRequest(ResourceMap[parameters.GetType()], Method.GET);
            AddQueryParameters(parameters, request);

            var response = ExecuteAsync<Response>(request);
            return response;
        }

        private void AddQueryParameters(FindLocationParameters parameters, IRestRequest request)
        {
            AddOptionalQueryParameters(parameters, request);

            foreach (var parametersBuilder in ParameterBuilders)
            {
                parametersBuilder.Build(parameters, request);
            }
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

            if (parameters.IncludeAdditionalInformation != null)
            {
                request.AddQueryParameter("incl", parameters.IncludeAdditionalInformation.ToCSVString());
            }
        }
    }
}
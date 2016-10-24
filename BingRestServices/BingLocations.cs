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
        public BingLocations()
        {
        }

        public BingLocations(BingConfiguration configuration)
            : base(configuration)
        {
        }

        public Task<Response> FindLocationAsync(FindLocationParameters parameters)
        {
            var request = new RestRequest("Locations", Method.GET);

            AddAddressQueryParameters(parameters.Address, request);
            AddOptionalQueryParameters(parameters, request);

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

        private void AddAddressQueryParameters(GeoAddress address, IRestRequest request)
        {
            if (!string.IsNullOrEmpty(address.AdminDistrict))
            {
                request.AddQueryParameter("adminDistrict", address.AdminDistrict);
            }
            
            if (!string.IsNullOrEmpty(address.Locality))
            {
                request.AddQueryParameter("locality", address.Locality);
            }
            
            if (!string.IsNullOrEmpty(address.PostalCode))
            {
                request.AddQueryParameter("postalCode", address.PostalCode);
            }
            
            if (!string.IsNullOrEmpty(address.AddressLine))
            {
                request.AddQueryParameter("addressLine", address.AddressLine);
            }

            if (!string.IsNullOrEmpty(address.CountryRegion))
            {
                request.AddQueryParameter("countryRegion", address.CountryRegion);
            }
        }
    }
}
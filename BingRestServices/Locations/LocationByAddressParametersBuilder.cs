using RestSharp;

namespace BingRestServices.Locations
{
    public class LocationByAddressParametersBuilder : ILocationParametersBuilder, ILocationParametersBuilder<FindLocationByAddressParameters>
    {
        public void Build(FindLocationParameters parameters, IRestRequest request)
        {
            if (parameters is FindLocationByAddressParameters)
            {
                Build(parameters as FindLocationByAddressParameters, request);
            }
        }

        public void Build(FindLocationByAddressParameters parameters, IRestRequest request)
        {
            var address = parameters.Address;
            if (address == null) return;

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
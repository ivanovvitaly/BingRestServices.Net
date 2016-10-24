using System.Linq;

using BingRestServices.DataContracts;

namespace BingRestServices
{
    public class GeoAddress : Address, IGeoLocation
    {
        public string GetFormattedString()
        {
            if (!string.IsNullOrEmpty(Landmark))
            {
                return Landmark;
            }

            var values = new[] { AddressLine, Locality, AdminDistrict, PostalCode };

            return string.Join(", ", values.Where(p => !string.IsNullOrEmpty(p)).ToArray());
        }


        public static GeoAddress CreateLandmark(string landmark)
        {
            return new GeoAddress { Landmark = landmark };
        }

        public static GeoAddress CreateAddress(string city, string state)
        {
            return CreateAddress(city, state, null);
        }

        public static GeoAddress CreateAddress(string city, string state, string postalCode)
        {
            return CreateAddress(null, city, state, postalCode);
        }

        public static GeoAddress CreateAddress(string addressLine, string city, string state, string postalCode)
        {
            return new GeoAddress
            {
                AddressLine = addressLine,
                Locality = city,
                AdminDistrict = state,
                PostalCode = postalCode
            };
        }
        
        public static GeoAddress CreateAddress(string addressLine, string city, string state, string postalCode, string isoCountryCode)
        {
            return new GeoAddress
            {
                AddressLine = addressLine,
                Locality = city,
                AdminDistrict = state,
                PostalCode = postalCode,
                CountryRegion = isoCountryCode
            };
        }
    }
}
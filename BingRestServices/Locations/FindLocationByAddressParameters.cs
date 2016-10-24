namespace BingRestServices.Locations
{
    public class FindLocationByAddressParameters : FindLocationParameters
    {
        public GeoAddress Address { get; set; }
    }
}
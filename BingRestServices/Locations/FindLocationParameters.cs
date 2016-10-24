namespace BingRestServices.Locations
{
    public class FindLocationParameters
    {
        public GeoAddress Address { get; set; }

        public IncludeNeighborhood IncludeNeighborhood { get; set; }

        public IncludeCountryISOCode IncludeCountryIsoCode { get; set; }

        public MaxResults MaxResults { get; set; }
    }
}
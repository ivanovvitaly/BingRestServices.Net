namespace BingRestServices.Locations
{
    public abstract class FindLocationParameters
    {
        public IncludeNeighborhood IncludeNeighborhood { get; set; }

        public IncludeCountryISOCode IncludeCountryIsoCode { get; set; }

        public MaxResults MaxResults { get; set; }
    }
}
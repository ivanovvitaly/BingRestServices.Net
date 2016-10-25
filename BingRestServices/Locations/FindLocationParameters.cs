namespace BingRestServices.Locations
{
    public abstract class FindLocationParameters
    {
        public IncludeNeighborhood IncludeNeighborhood { get; set; }

        public LocationAdditionalInfomation[] IncludeAdditionalInformation { get; set; }

        public MaxResults MaxResults { get; set; }
    }
}
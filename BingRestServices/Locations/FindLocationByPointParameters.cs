namespace BingRestServices.Locations
{
    public class FindLocationByPointParameters : FindLocationParameters
    {
        public GeoPoint Point { get; set; }

        public IncludeEntityType[] IncludeEntityTypes { get; set; }
    }
}
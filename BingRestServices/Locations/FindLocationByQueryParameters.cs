namespace BingRestServices.Locations
{
    public class FindLocationByQueryParameters : FindLocationParameters
    {
        public GeoAddress Query { get; set; }
    }
}
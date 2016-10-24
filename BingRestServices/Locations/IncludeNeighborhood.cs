namespace BingRestServices.Locations
{
    public class IncludeNeighborhood : KeyType
    {
        public static readonly IncludeNeighborhood DoNotInclude  = Create<IncludeNeighborhood>("0");
        public static readonly IncludeNeighborhood Include = Create<IncludeNeighborhood>("1");
    }
}
namespace BingRestServices.Locations
{
    public class IncludeEntityType : KeyType
    {
        public static readonly IncludeEntityType Address = Create<IncludeEntityType>("Address");
        public static readonly IncludeEntityType Neighborhood = Create<IncludeEntityType>("Neighborhood");
        public static readonly IncludeEntityType PopulatedPlace = Create<IncludeEntityType>("PopulatedPlace");
        public static readonly IncludeEntityType Postcode1 = Create<IncludeEntityType>("Postcode1");
        public static readonly IncludeEntityType AdminDivision1 = Create<IncludeEntityType>("AdminDivision1");
        public static readonly IncludeEntityType AdminDivision2 = Create<IncludeEntityType>("AdminDivision2");
        public static readonly IncludeEntityType CountryRegion = Create<IncludeEntityType>("CountryRegion");
    }
}
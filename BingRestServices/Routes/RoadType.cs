namespace BingRestServices.Routes
{
    public class RoadType: KeyType
    {
        public static readonly RoadType Highways = Create<RoadType>("highways");
        public static readonly RoadType Tolls = Create<RoadType>("tolls");
        public static readonly RoadType MinimizeHighways = Create<RoadType>("minimizeHighways");
        public static readonly RoadType MinimizeTolls = Create<RoadType>("minimizeTolls");
    }
}
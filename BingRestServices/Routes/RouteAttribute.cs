namespace BingRestServices.Routes
{
    public class RouteAttribute : KeyType
    {
        public static readonly RouteAttribute ExcludeItinerary = Create<RouteAttribute>("excludeItinerary");
        public static readonly RouteAttribute RoutePath = Create<RouteAttribute>("routePath");
        public static readonly RouteAttribute TransitStops = Create<RouteAttribute>("transitStops");
        public static readonly RouteAttribute RouteSummariesOnly = Create<RouteAttribute>("routeSummariesOnly");
        public static readonly RouteAttribute All = Create<RouteAttribute>("all");
    }
}
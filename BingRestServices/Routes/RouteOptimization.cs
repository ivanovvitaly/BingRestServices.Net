namespace BingRestServices.Routes
{
    public class RouteOptimization : KeyType
    {
        public static readonly RouteOptimization Distance = Create<RouteOptimization>("distance");
        public static readonly RouteOptimization Time = Create<RouteOptimization>("time");
        public static readonly RouteOptimization TimeWithTraffic = Create<RouteOptimization>("timeWithTraffic");
        public static readonly RouteOptimization TimeAvoidClosure = Create<RouteOptimization>("timeAvoidClosure");
    }
}
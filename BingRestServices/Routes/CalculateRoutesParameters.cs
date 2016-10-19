using System;

namespace BingRestServices.Routes
{
    public class CalculateRoutesParameters
    {
        public IGeoLocation[] WayPoints { get; set; }

        public IGeoLocation[] ViaWayPoints { get; set; }

        public RoadType[] AvoidRoadTypes { get; set; }

        public int? DistanceBeforeFirstTurn { get; set; }

        public int? Heading { get; set; }

        public RouteOptimization RouteOptimization { get; set; }

        public RouteAttribute[] RouteAttributes { get; set; }

        public double[] Tolerances { get; set; }

        public DistanceUnite DistanceUnite { get; set; }

        public DateTime? DesireTransiteTime { get; set; }

        public TransiteTimeType TransiteTimeType { get; set; }

        public MaxSolutions MaxSolutions { get; set; }

        public TravelMode TravelMode { get; set; }
    }
}
namespace BingRestServices.Traffic
{
    public class TrafficIncidentsParameters
    {
        public TrafficIncidentsParameters(MapArea mapArea)
        {
            MapArea = mapArea;
        }

        public MapArea MapArea { get; set; }

        public bool? IncludeLocationCodes { get; set; }

        public Severity[] Severity { get; set; }

        public TrafficIncidentType[] TrafficIncidentTypes { get; set; } 
    }
}
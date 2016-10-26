namespace BingRestServices.Traffic
{
    public class TrafficIncidentType : KeyType
    {
        public static readonly TrafficIncidentType Accident = Create<TrafficIncidentType>("1");
        public static readonly TrafficIncidentType Congestion = Create<TrafficIncidentType>("2");
        public static readonly TrafficIncidentType DisabledVehicle = Create<TrafficIncidentType>("3");
        public static readonly TrafficIncidentType MassTransit = Create<TrafficIncidentType>("4");
        public static readonly TrafficIncidentType Miscellaneous = Create<TrafficIncidentType>("5");
        public static readonly TrafficIncidentType OtherNews = Create<TrafficIncidentType>("6");
        public static readonly TrafficIncidentType PlannedEvent = Create<TrafficIncidentType>("7");
        public static readonly TrafficIncidentType RoadHazard = Create<TrafficIncidentType>("8");
        public static readonly TrafficIncidentType Construction = Create<TrafficIncidentType>("9");
        public static readonly TrafficIncidentType Alert = Create<TrafficIncidentType>("10");
        public static readonly TrafficIncidentType Weather = Create<TrafficIncidentType>("11");
    }
}
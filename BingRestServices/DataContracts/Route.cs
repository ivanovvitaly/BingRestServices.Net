using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    public class Route : Resource
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "distanceUnit", EmitDefaultValue = false)]
        public string DistanceUnit { get; set; }

        [DataMember(Name = "durationUnit", EmitDefaultValue = false)]
        public string DurationUnit { get; set; }

        [DataMember(Name = "travelDistance", EmitDefaultValue = false)]
        public double TravelDistance { get; set; }

        [DataMember(Name = "travelDuration", EmitDefaultValue = false)]
        public double TravelDuration { get; set; }

        [DataMember(Name = "routeLegs", EmitDefaultValue = false)]
        public RouteLeg[] RouteLegs { get; set; }

        [DataMember(Name = "routePath", EmitDefaultValue = false)]
        public RoutePath RoutePath { get; set; }
    }
}
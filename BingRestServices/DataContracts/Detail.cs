using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class Detail
    {
        [DataMember(Name = "compassDegrees", EmitDefaultValue = false)]
        public int CompassDegrees { get; set; }

        [DataMember(Name = "maneuverType", EmitDefaultValue = false)]
        public string ManeuverType { get; set; }

        [DataMember(Name = "startPathIndices", EmitDefaultValue = false)]
        public int[] StartPathIndices { get; set; }

        [DataMember(Name = "endPathIndices", EmitDefaultValue = false)]
        public int[] EndPathIndices { get; set; }

        [DataMember(Name = "roadType", EmitDefaultValue = false)]
        public string RoadType { get; set; }

        [DataMember(Name = "locationCodes", EmitDefaultValue = false)]
        public string[] LocationCodes { get; set; }

        [DataMember(Name = "names", EmitDefaultValue = false)]
        public string[] Names { get; set; }

        [DataMember(Name = "mode", EmitDefaultValue = false)]
        public string Mode { get; set; }

        [DataMember(Name = "roadShieldRequestParameters", EmitDefaultValue = false)]
        public RoadShield RoadShieldRequestParameters { get; set; }
    }
}
using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class Instruction
    {
        [DataMember(Name = "maneuverType", EmitDefaultValue = false)]
        public string ManeuverType { get; set; }

        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }
    }
}
using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class RoutePath
    {
        [DataMember(Name = "line", EmitDefaultValue = false)]
        public Line Line { get; set; }

        [DataMember(Name = "generalizations", EmitDefaultValue = false)]
        public Generalization[] Generalizations { get; set; }
    }
}
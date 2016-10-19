using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class Generalization
    {
        [DataMember(Name = "pathIndices", EmitDefaultValue = false)]
        public int[] PathIndices { get; set; }

        [DataMember(Name = "latLongTolerance", EmitDefaultValue = false)]
        public double LatLongTolerance { get; set; }
    }
}
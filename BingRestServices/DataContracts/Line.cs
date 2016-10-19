using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class Line
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public double[][] Coordinates { get; set; }
    }
}
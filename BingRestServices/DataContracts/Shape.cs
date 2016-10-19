using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    [KnownType(typeof(Point))]
    public class Shape
    {
        [DataMember(Name = "boundingBox", EmitDefaultValue = false)]
        public double[] BoundingBox { get; set; }
    }
}
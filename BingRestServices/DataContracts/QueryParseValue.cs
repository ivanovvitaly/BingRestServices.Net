using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class QueryParseValue
    {
        [DataMember(Name = "property", EmitDefaultValue = false)]
        public string Property { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
}
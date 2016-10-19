﻿using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    public class ResourceSet
    {
        [DataMember(Name = "estimatedTotal", EmitDefaultValue = false)]
        public long EstimatedTotal { get; set; }

        [DataMember(Name = "resources", EmitDefaultValue = false)]
        public Resource[] Resources { get; set; }
    }
}
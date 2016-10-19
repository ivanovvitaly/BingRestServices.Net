﻿using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract]
    [KnownType(typeof(Location))]
    [KnownType(typeof(Route))]
    [KnownType(typeof(TrafficIncident))]
    [KnownType(typeof(ImageryMetadata))]
    [KnownType(typeof(ElevationData))]
    [KnownType(typeof(SeaLevelData))]
    [KnownType(typeof(CompressedPointList))]
    public class Resource
    {
        [DataMember(Name = "bbox", EmitDefaultValue = false)]
        public double[] BoundingBox { get; set; }

        [DataMember(Name = "__type", EmitDefaultValue = false)]
        public string Type { get; set; }
    }
}
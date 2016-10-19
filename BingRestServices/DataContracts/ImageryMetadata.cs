﻿using System.Runtime.Serialization;

namespace BingRestServices.DataContracts
{
    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    [KnownType(typeof(StaticMapMetadata))]
    [KnownType(typeof(BirdseyeMetadata))]
    public class ImageryMetadata : Resource
    {
        [DataMember(Name = "imageHeight", EmitDefaultValue = false)]
        public string ImageHeight { get; set; }

        [DataMember(Name = "imageWidth", EmitDefaultValue = false)]
        public string ImageWidth { get; set; }

        [DataMember(Name = "imageUrl", EmitDefaultValue = false)]
        public string ImageUrl { get; set; }

        [DataMember(Name = "imageUrlSubdomains", EmitDefaultValue = false)]
        public string[] ImageUrlSubdomains { get; set; }

        [DataMember(Name = "vintageEnd", EmitDefaultValue = false)]
        public string VintageEnd { get; set; }

        [DataMember(Name = "vintageStart", EmitDefaultValue = false)]
        public string VintageStart { get; set; }

        [DataMember(Name = "zoomMax", EmitDefaultValue = false)]
        public int ZoomMax { get; set; }

        [DataMember(Name = "zoomMin", EmitDefaultValue = false)]
        public int ZoomMin { get; set; }
    }
}
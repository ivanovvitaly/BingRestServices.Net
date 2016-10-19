using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

using RestSharp;
using RestSharp.Deserializers;

namespace BingRestServices.Serialization
{
    public class RestSharpDataContractJsonDeserializer : IDeserializer
    {
        public RestSharpDataContractJsonDeserializer()
        {
            Culture = CultureInfo.InvariantCulture;
        }

        public CultureInfo Culture { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            using(MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response.Content)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                T target = (T)serializer.ReadObject(ms);

                return target;
            }
        }

        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }
    }
}
using System.Configuration;

namespace BingRestServices.Configuration
{
    public class BingConfigurationSection : ConfigurationSection
    {
        public static readonly string BingConfigurationSectionName = "bingConfiguration";

        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseUrl
        {
            get { return this["baseUrl"] as string; }
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return this["key"] as string; }
        }

        [ConfigurationProperty("version", IsRequired = false)]
        public string Version
        {
            get { return this["version"] as string; }
        }

        [ConfigurationProperty("output", IsRequired = false)]
        public string OutputFormat
        {
            get { return this["output"] as string; }
        }

        [ConfigurationProperty("errorDetail", IsRequired = false)]
        public bool? ErrorDetail
        {
            get { return this["errorDetail"] as bool?; }
        }

        [ConfigurationProperty("culture", IsRequired = false)]
        public string Culture
        {
            get { return this["culture"] as string; }
        }

        [ConfigurationProperty("suppressStatus", IsRequired = false)]
        public bool? SuppressStatus
        {
            get { return this["suppressStatus"] as bool?; }
        }

        [ConfigurationProperty("jsonpCallback", IsRequired = false)]
        public string JsonpCallback
        {
            get { return this["jsonpCallback"] as string; }
        }

        [ConfigurationProperty("jsonStateObject", IsRequired = false)]
        public string JsonStateObject
        {
            get { return this["jsonStateObject"] as string; }
        }
    }
}
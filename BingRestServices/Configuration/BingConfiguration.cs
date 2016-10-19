namespace BingRestServices.Configuration
{
    public class BingConfiguration
    {
        public BingConfiguration(string key)
        {
            Key = key;
            BaseUrl = "http://dev.virtualearth.net/REST/{version}/";
            Version = "v1";
        }
        
        public BingConfiguration(string key, string baseUrl) : this(key)
        {
            BaseUrl = baseUrl;
            Version = "v1";
        }
        
        public BingConfiguration(string key, string baseUrl, string version) : this(key, baseUrl)
        {
            Version = version;
        }

        public string Key { get; set; }

        public string BaseUrl { get; set; }

        public string Version { get; set; }

        public string OutputFormat { get; set; }

        public bool? ErrorDetail { get; set; }

        public string Culture { get; set; }

        public bool? SuppressStatus { get; set; }

        public string JsonpCallback { get; set; }

        public string JsonStateObject { get; set; }


        public static BingConfiguration CreateJsonOutputConfiguration(string key)
        {
            var config = new BingConfiguration(key);
            config.OutputFormat = "json";
            config.Culture = "en-US";

            return config;
        }

        public static BingConfiguration CreateFromConfiguratioSection(BingConfigurationSection section)
        {
            var config = new BingConfiguration(section.Key);
            config.BaseUrl = section.BaseUrl;
            config.Version = section.Version;
            config.Culture = section.Culture;
            config.OutputFormat = section.OutputFormat;
            config.ErrorDetail = section.ErrorDetail;
            config.JsonpCallback = section.JsonpCallback;
            config.JsonStateObject = section.JsonStateObject;
            config.SuppressStatus = section.SuppressStatus;

            return config;
        }
    }
}
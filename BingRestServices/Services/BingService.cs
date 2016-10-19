using System;
using System.Configuration;
using System.Threading.Tasks;

using BingRestServices.Configuration;
using BingRestServices.DataContracts;
using BingRestServices.Exceptions;
using BingRestServices.Serialization;

using RestSharp;

namespace BingRestServices.Services
{
    public abstract class BingService
    {
        private readonly string ErrorMessage = "Error retrieving response. Check inner details for more info.";
        private readonly BingConfiguration configuration;

        #region // ctor 

        protected BingService()
        {
            var configurationSection =
                (BingConfigurationSection)
                ConfigurationManager.GetSection(BingConfigurationSection.BingConfigurationSectionName);

            if (configurationSection == null)
            {
                throw new BingConfigurationException(
                    string.Format(
                        "Invalid Bing configuration. Insure you have '{0}' configuration sections defined in the application config file.",
                        BingConfigurationSection.BingConfigurationSectionName));
            }

            configuration = BingConfiguration.CreateFromConfiguratioSection(configurationSection);
        }

        protected BingService(BingConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new BingConfigurationException(
                    "Invalid Bing configuration.",
                    new ArgumentNullException(
                        "configuration",
                        "Bing configuration object is required. Provide valid BingConfiguration object to instantiate the service."));
            }

            this.configuration = configuration;
        }

        #endregion

        public virtual T Execute<T>(IRestRequest request) where T : new()
        {
            var client = CreateRestClient();
            var response = client.Execute<T>(request);

            return GetResponseData(response);
        }

        public virtual async Task<T> ExecuteAsync<T>(IRestRequest request) where T : new()
        {
            var client = CreateRestClient();
            var response = await client.ExecuteTaskAsync<T>(request);

            return GetResponseData(response);
        }

        private IRestClient CreateRestClient()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(configuration.BaseUrl);
            
            client.AddHandler("application/json", new RestSharpDataContractJsonDeserializer());
            
            client.AddDefaultParameter("key", configuration.Key, ParameterType.QueryString);
            client.AddDefaultUrlSegment("version", configuration.Version);
            if (configuration.ErrorDetail.HasValue) client.AddDefaultUrlSegment("ed", configuration.ErrorDetail.Value.ToString());
            if (configuration.SuppressStatus.HasValue) client.AddDefaultUrlSegment("ss", configuration.SuppressStatus.Value.ToString());
            if (!string.IsNullOrEmpty(configuration.OutputFormat)) client.AddDefaultUrlSegment("o", configuration.OutputFormat);
            if (!string.IsNullOrEmpty(configuration.JsonpCallback)) client.AddDefaultUrlSegment("jsonp", configuration.JsonpCallback);
            if (!string.IsNullOrEmpty(configuration.JsonStateObject)) client.AddDefaultUrlSegment("jsonso", configuration.JsonStateObject);
            if (!string.IsNullOrEmpty(configuration.Culture)) client.AddDefaultUrlSegment("c", configuration.Culture);

            return client;
        }

        private T GetResponseData<T>(IRestResponse<T> response)
        {
            if (response.ErrorException != null)
            {
                throw new BingRestResponseException(ErrorMessage, response.ErrorException);
            }

            if (response.Data is Response)
            {
                return response.Data;
            }

            throw new BingRestResponseException(response.StatusDescription);
        }
    }
}
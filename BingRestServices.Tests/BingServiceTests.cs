using System;
using System.Configuration;
using System.Xml;

using BingRestServices.Configuration;
using BingRestServices.Exceptions;
using BingRestServices.Services;

using Moq;

using NUnit.Framework;

namespace BingRestServices.Tests
{
    [TestFixture]
    public class BingServiceTests
    {
        [Test]
        public void CreateServiceWithDefaultConstructor_NullAppConfigBingConfiguration_BingConfigurationException()
        {
            var originalAppConfig = new XmlDocument();
            originalAppConfig.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            try
            {
                var appConfig = new XmlDocument();
                appConfig.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                var bingConfigSectionDefinition = appConfig.SelectSingleNode(string.Format("//configSections/section[@name='{0}']", BingConfigurationSection.BingConfigurationSectionName));
                bingConfigSectionDefinition.ParentNode.RemoveChild(bingConfigSectionDefinition);
                var bingConfigurationSection = appConfig.SelectSingleNode(string.Format("//{0}", BingConfigurationSection.BingConfigurationSectionName));
                bingConfigurationSection.ParentNode.RemoveChild(bingConfigurationSection);
                appConfig.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection(BingConfigurationSection.BingConfigurationSectionName);
                ConfigurationManager.RefreshSection("configSections");

                var instance = new Mock<BingService>().Object;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is BingConfigurationException)
                {
                    Assert.Pass();
                }       
            }
            finally
            {
                // restoring original app.config file.
                originalAppConfig.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection(BingConfigurationSection.BingConfigurationSectionName);
                ConfigurationManager.RefreshSection("configSections");
            }

            Assert.Fail();
        }
        
        [Test]
        public void CreateServiceBingConfigurationParameterConstructor_NullBingConfiguration_BingConfigurationException()
        {
            try
            {
                var instance = new Mock<BingService>((BingConfiguration)null).Object;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is BingConfigurationException)
                {
                    Assert.Pass();          
                }
            }

            Assert.Fail();
        }
        
        [Test]
        public void CreateServiceDefaultConstructor_AppConfigBingConfiguration_ServiceCreated()
        {
            var service = new Mock<BingService>().Object;

            Assert.That(service, Is.Not.Null);
        }
        
        [Test]
        public void CreateServiceBingConfigurationParameterConstructor_ValidConfiguration_ServiceCreated()
        {
            var bingConfiguration = BingConfiguration.CreateJsonOutputConfiguration("samplekey");
            var service = new Mock<BingService>(bingConfiguration).Object;

            Assert.That(service, Is.Not.Null);
        }
    }
}

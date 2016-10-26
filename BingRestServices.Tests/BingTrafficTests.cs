using System;
using System.Linq;
using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Extensions;
using BingRestServices.Traffic;

using Moq;

using NUnit.Framework;

using RestSharp;

namespace BingRestServices.Tests
{
    [TestFixture]
    public class BingTrafficTests
    {
        [Test]
        public async Task GetTrafficIncidents_NullTrafficIncidentsParameters_ArgumentNullException()
        {
            var serviceMock = new Mock<BingTraffic>();
            var service = serviceMock.Object;

            try
            {
                var response = await service.GetTrafficIncidents(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
        
        [Test]
        public async Task GetTrafficIncidents_NullMapArea_ArgumentNullException()
        {
            var serviceMock = new Mock<BingTraffic>();
            var service = serviceMock.Object;
            var parameters = new TrafficIncidentsParameters(null);

            try
            {
                var response = await service.GetTrafficIncidents(parameters);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
        
        [Test]
        public async Task GetTrafficIncidents_MapArea_ValidTrafficIncidents()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingTraffic>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var mapArea = new MapArea(37,-105,45,-94);
            var parameters = new TrafficIncidentsParameters(mapArea);

            var response = await service.GetTrafficIncidents(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<TrafficIncident>().Count(), Is.GreaterThan(0));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Traffic/Incidents/{MapArea}/"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "MapArea"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "MapArea").Value, Is.EqualTo(mapArea.ToString()));
        }
        
        [Test]
        public async Task GetTrafficIncidents_IncidentTypesSeverityMapArea_ValidTrafficIncidents()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingTraffic>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var mapArea = new MapArea(37,-105,45,-94);
            var parameters = new TrafficIncidentsParameters(mapArea);
            parameters.IncludeLocationCodes = true;
            parameters.Severity = new[] { Severity.Minor, Severity.Moderate };
            parameters.TrafficIncidentTypes = new[] { TrafficIncidentType.Construction };

            var response = await service.GetTrafficIncidents(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<TrafficIncident>().Count(), Is.GreaterThan(0));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Traffic/Incidents/{MapArea}/"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "MapArea"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "MapArea").Value, Is.EqualTo(mapArea.ToString()));
            Assert.That(request.Parameters.Find(x => x.Name == "includeLocationCodes"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "includeLocationCodes").Value, Is.EqualTo(parameters.IncludeLocationCodes.ToString()));
            Assert.That(request.Parameters.Find(x => x.Name == "s"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "s").Value, Is.EqualTo(parameters.Severity.ToCSVString()));
            Assert.That(request.Parameters.Find(x => x.Name == "t"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "t").Value, Is.EqualTo(parameters.TrafficIncidentTypes.ToCSVString()));
        }
    }
}
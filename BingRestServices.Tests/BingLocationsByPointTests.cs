using System;
using System.Linq;
using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Locations;

using Moq;

using NUnit.Framework;

using RestSharp;

namespace BingRestServices.Tests
{
    [TestFixture]
    public class BingLocationsByPointTests
    {
        [Test]
        public async Task FindLocationAsync_NullPoint_ArgumentNullException()
        {
            var serviceMock = new Mock<BingLocations>();
            var service = serviceMock.Object;
            var parameters = new FindLocationByPointParameters();
            parameters.Point = null;

            try
            {
                var response = await service.FindLocationAsync(parameters);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public async Task FindLocationAsync_ValidPoint_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByPointParameters();
            parameters.Point = GeoPoint.Create(47.64054, -122.12934);

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(), Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().First().Name, Is.EqualTo("Microsoft Way, Redmond, WA 98052"));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations/{Point}"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "Point"), Is.Not.Null);
        }

        [Test]
        public async Task FindLocationAsync_ValidPointIncludingCountryRegion_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByPointParameters();
            parameters.Point = GeoPoint.Create(47.64054, -122.12934);
            parameters.IncludeEntityTypes = new[] { IncludeEntityType.CountryRegion };

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(), Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().First().Name, Is.EqualTo("United States"));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations/{Point}"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "Point"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "includeEntityTypes"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "includeEntityTypes").Value, Is.EqualTo(IncludeEntityType.CountryRegion.Key));
        }
    }
}
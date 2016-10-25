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
    public class BingLocationsByQueryTests
    {
        [Test]
        public async Task FindLocationAsync_NullQuery_ArgumentNullException()
        {
            var serviceMock = new Mock<BingLocations>();
            var service = serviceMock.Object;
            var parameters = new FindLocationByQueryParameters();
            parameters.Query = null;

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
        public async Task FindLocationAsync_ValidUSAddressQuery_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByQueryParameters();
            parameters.Query = GeoAddress.CreateAddress(
                "1 Microsoft Way",
                "Redmond",
                "WA",
                "98052",
                "US");

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(), Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().First().Name, Is.EqualTo("1 Microsoft Way, Redmond, WA 98052"));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q").Value, Is.EqualTo(parameters.Query.GetFormattedString()));
        }

        [Test]
        public async Task FindLocationAsync_ValidUSAddressQueryIncludingQueryParser_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByQueryParameters();
            parameters.IncludeAdditionalInformation = new[] { LocationAdditionalInfomation.QueryParser };
            parameters.Query = GeoAddress.CreateAddress(
                "1 Microsoft Way",
                "Redmond",
                "WA",
                "98052",
                "US");

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(), Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().First().Name, Is.EqualTo("1 Microsoft Way, Redmond, WA 98052"));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().First().QueryParseValues, Is.Not.Null);
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().First().QueryParseValues.Length, Is.GreaterThan(0));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q").Value, Is.EqualTo(parameters.Query.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "incl"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "incl").Value, Is.EqualTo(LocationAdditionalInfomation.QueryParser.Key));
        }

        [Test]
        public async Task FindLocationAsync_ValidLandMarkQueryIncludingNeighborhood_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByQueryParameters();
            parameters.IncludeNeighborhood = IncludeNeighborhood.Include;
            parameters.Query = GeoAddress.CreateLandmark("Brookyln New York");

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().EstimatedTotal, Is.EqualTo(3));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(p => p.EntityType == "PopulatedPlace"), Is.EqualTo(1));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(p => p.EntityType == "Neighborhood"), Is.EqualTo(2));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q").Value, Is.EqualTo(parameters.Query.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "inclnb"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "inclnb").Value, Is.EqualTo(IncludeNeighborhood.Include.Key));
        }

        [Test]
        public async Task FindLocationAsync_ValidLandMarkQuery_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByQueryParameters();
            parameters.Query = GeoAddress.CreateLandmark("Eiffel Tower");

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(), Is.GreaterThan(0));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q").Value, Is.EqualTo(parameters.Query.GetFormattedString()));
        }

        [Test]
        public async Task FindLocationAsync_ValidLocationQueryMaxResults10_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByQueryParameters();
            parameters.MaxResults = new MaxResults(10);
            parameters.Query = GeoAddress.CreateLandmark("Greenville");

            var response = await service.FindLocationAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets.First().Resources.OfType<Location>().Count(), Is.EqualTo(10));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Locations"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "q").Value, Is.EqualTo(parameters.Query.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "maxRes"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "maxRes").Value, Is.EqualTo(new MaxResults(10).Key));
        }
    }
}
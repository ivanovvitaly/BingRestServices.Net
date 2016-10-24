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
    public class BingLocationsTests
    {
        [Test]
        public async Task FindLocationAsync_ValidAddress_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationParameters();
            parameters.Address = GeoAddress.CreateAddress(
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
            Assert.That(request.Parameters.Find(x => x.Name == "adminDistrict"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "adminDistrict").Value, Is.EqualTo(parameters.Address.AdminDistrict));
            Assert.That(request.Parameters.Find(x => x.Name == "locality"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "locality").Value, Is.EqualTo(parameters.Address.Locality));
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode").Value, Is.EqualTo(parameters.Address.PostalCode));
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine").Value, Is.EqualTo(parameters.Address.AddressLine));
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
        }
    }
}
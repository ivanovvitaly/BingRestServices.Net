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
    public class BingLocationsTests
    {
        [Test]
        public async Task FindLocationAsync_ValidUSAddressIncludingPostalCode_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
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
        
        [Test]
        public async Task FindLocationAsync_ValidUSAddressWithoutPostalCode_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.AddressLine = "1 Microsoft Way";
            parameters.Address.AdminDistrict = "WA";
            parameters.Address.CountryRegion = "US";
            parameters.Address.Locality = "Redmond";

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
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine").Value, Is.EqualTo(parameters.Address.AddressLine));
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
        }
        
        [Test]
        public async Task FindLocationAsync_LocalityOnlyMaxResult10_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.MaxResults = new MaxResults(10);
            parameters.Address = new GeoAddress();
            parameters.Address.Locality = "Greenville";

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
            Assert.That(request.Parameters.Find(x => x.Name == "locality"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "locality").Value, Is.EqualTo(parameters.Address.Locality));
            Assert.That(request.Parameters.Find(x => x.Name == "maxRes"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "maxRes").Value, Is.EqualTo("10"));
        }
        
        [Test]
        public async Task FindLocationAsync_ValidUSAddressWithNeighborhood_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.IncludeNeighborhood = IncludeNeighborhood.Include;
            parameters.Address.Locality = "Ballard";
            parameters.Address.AdminDistrict = "WA";
            parameters.Address.CountryRegion = "US";

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
            Assert.That(request.Parameters.Find(x => x.Name == "locality"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "locality").Value, Is.EqualTo(parameters.Address.Locality));
            Assert.That(request.Parameters.Find(x => x.Name == "inclnb"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "inclnb").Value, Is.EqualTo("1"));
        }
        
        [Test]
        public async Task FindLocationAsync_ValidCanadaAddress_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.CountryRegion = "CA";
            parameters.Address.AdminDistrict = "BC";
            parameters.Address.PostalCode = "V6G";
            parameters.Address.Locality = "Vancouver";
            parameters.Address.AddressLine = "Stanley Park Causeway";

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
            Assert.That(request.Parameters.Find(x => x.Name == "adminDistrict"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "adminDistrict").Value, Is.EqualTo(parameters.Address.AdminDistrict));
            Assert.That(request.Parameters.Find(x => x.Name == "locality"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "locality").Value, Is.EqualTo(parameters.Address.Locality));
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine").Value, Is.EqualTo(parameters.Address.AddressLine));
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode").Value, Is.EqualTo(parameters.Address.PostalCode));
        }
        
        [Test]
        public async Task FindLocationAsync_ValidFranceAddress_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.CountryRegion = "FR";
            parameters.Address.PostalCode = "75007";
            parameters.Address.Locality = "Paris";
            parameters.Address.AddressLine = "Avenue Gustave Eiffel";

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
            Assert.That(request.Parameters.Find(x => x.Name == "locality"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "locality").Value, Is.EqualTo(parameters.Address.Locality));
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine").Value, Is.EqualTo(parameters.Address.AddressLine));
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode").Value, Is.EqualTo(parameters.Address.PostalCode));
        }
        
        [Test]
        public async Task FindLocationAsync_ValidGermanyAddress_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.CountryRegion = "DE";
            parameters.Address.PostalCode = "12010";
            parameters.Address.Locality = "Berlin";
            parameters.Address.AddressLine = "Platz Der Luftbrücke 5";

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
            Assert.That(request.Parameters.Find(x => x.Name == "locality"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "locality").Value, Is.EqualTo(parameters.Address.Locality));
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine").Value, Is.EqualTo(parameters.Address.AddressLine));
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode").Value, Is.EqualTo(parameters.Address.PostalCode));
        }
        
        [Test]
        public async Task FindLocationAsync_ValidUKAddress_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.CountryRegion = "GB";
            parameters.Address.PostalCode = "SW1A";

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
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode").Value, Is.EqualTo(parameters.Address.PostalCode));
        }
        
        [Test]
        public async Task FindLocationAsync_ValidUKAddress2_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.CountryRegion = "GB";
            parameters.Address.PostalCode = "SW1A 2AA";

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
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "countryRegion").Value, Is.EqualTo(parameters.Address.CountryRegion));
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "postalCode").Value, Is.EqualTo(parameters.Address.PostalCode));
        }

        [Test, Ignore("Not implemented yet")]
        public async Task FindLocationAsync_ValidUKAddressWithUserLocation_ValidLocation()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingLocations>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var parameters = new FindLocationByAddressParameters();
            parameters.Address = new GeoAddress();
            parameters.Address.AddressLine = "Kings Road";
            //parameters.UserLocation = GeoPoint.Create(51.504360719046616, -0.12600176611298197);

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
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "addressLine").Value, Is.EqualTo(parameters.Address.AddressLine));
            //Assert.That(request.Parameters.Find(x => x.Name == "userLocation"), Is.Not.Null);
            //Assert.That(request.Parameters.Find(x => x.Name == "userLocation").Value, Is.EqualTo(parameters.UserLocation.ToString()));
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
            parameters.IncludeEntityTypes = new [] { IncludeEntityType.CountryRegion };

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
        public async Task FindLocationAsync_NullFindLocationParameters_ArgumentNullException()
        {
            var serviceMock = new Mock<BingLocations>();
            var service = serviceMock.Object;

            try
            {
                var response = await service.FindLocationAsync(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Pass();
            }
           
            Assert.Fail();
        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using BingRestServices.DataContracts;
using BingRestServices.Routes;

using Moq;

using NUnit.Framework;

using RestSharp;

namespace BingRestServices.Tests
{
    [TestFixture]
    public class BingRoutesTests
    {
        [Test]
        public async Task CalculateRoutesAsync_NullParameters_ArgumentNullException()
        {
            var service = new BingRoutes();

            try
            {
                var response = await service.CalculateRoutesAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public async Task CalculateRoutesAsync_ValidCalculateRoutesParameters_AllParametersSet()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingRoutes>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var ny = new GeoPoint(40.714545, -74.007139);
            var dc = new GeoPoint(38.890366, -77.031955);
            var philadelphia = new GeoPoint(39.952276, -75.162444);
            var parameters = new CalculateRoutesParameters();
            parameters.WayPoints = new[] { dc, null, ny };
            parameters.ViaWayPoints = new[] { null, philadelphia };
            parameters.MaxSolutions = MaxSolutions.One;
            parameters.TravelMode = TravelMode.Transit;
            parameters.RouteOptimization = RouteOptimization.Distance;
            parameters.AvoidRoadTypes = new[] { RoadType.Tolls, RoadType.Highways };
            parameters.DistanceBeforeFirstTurn = 100;
            parameters.DistanceUnite = DistanceUnite.Kilometer;
            parameters.Tolerances = new[] { 0.00000344978D };
            parameters.RouteAttributes = new[] { RouteAttribute.All };
            parameters.Heading = 90;
            parameters.DesireTransiteTime = DateTime.Today.AddDays(1);
            parameters.TransiteTimeType = TransiteTimeType.Departure;

            var response = await service.CalculateRoutesAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Routes"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0").Value, Is.EqualTo(dc.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "wp.2"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.2").Value, Is.EqualTo(ny.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "vwp.1"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "vwp.1").Value, Is.EqualTo(philadelphia.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "avoid"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "dbft"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "du"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "optmz"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "maxSolns"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "tl"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "ra"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "hd"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "dt"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "tt"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode"), Is.Not.Null);
        }

        [Test]
        public async Task CalculateRoutesAsync_DrivingRoute_ValidRoutes()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingRoutes>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var ny = new GeoPoint(40.714545, -74.007139);
            var dc = new GeoPoint(38.890366, -77.031955);
            var parameters = new CalculateRoutesParameters();
            parameters.WayPoints = new[] { dc, ny };
            parameters.AvoidRoadTypes = new RoadType[] { RoadType.MinimizeTolls };
            parameters.TravelMode = TravelMode.Driving;

            var response = await service.CalculateRoutesAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.EqualTo(1));
            Assert.That(response.ResourceSets[0].Resources.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets[0].Resources.All(r => r is Route), Is.EqualTo(true));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Routes"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0").Value, Is.EqualTo(dc.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1").Value, Is.EqualTo(ny.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "avoid"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "avoid").Value, Is.EqualTo(RoadType.MinimizeTolls.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode").Value, Is.EqualTo(TravelMode.Driving.Key));
        }

        [Test]
        public async Task CalculateRoutesAsync_WalkingRoute_ValidRoutes()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingRoutes>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var eiffelTower = GeoAddress.CreateLandmark("Eiffel Tower");
            var louvreMuseum = GeoAddress.CreateLandmark("louvre museum");
            var parameters = new CalculateRoutesParameters();
            parameters.WayPoints = new[] { eiffelTower, louvreMuseum };
            parameters.RouteOptimization = RouteOptimization.Distance;
            parameters.TravelMode = TravelMode.Walking;

            var response = await service.CalculateRoutesAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.EqualTo(1));
            Assert.That(response.ResourceSets[0].Resources.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets[0].Resources.All(r => r is Route), Is.EqualTo(true));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Routes"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0").Value, Is.EqualTo(eiffelTower.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1").Value, Is.EqualTo(louvreMuseum.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "optmz"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "optmz").Value, Is.EqualTo(RouteOptimization.Distance.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode").Value, Is.EqualTo(TravelMode.Walking.Key));
        }
        
        [Test]
        public async Task CalculateRoutesAsync_TransitRoute_ValidRoutes()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingRoutes>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var goldenGateBridge = GeoAddress.CreateLandmark("Golden Gate Bridge");
            var fishermansWharf = GeoAddress.CreateLandmark("Fishermans Wharf");
            var parameters = new CalculateRoutesParameters();
            parameters.WayPoints = new[] { goldenGateBridge, fishermansWharf };
            parameters.TransiteTimeType = TransiteTimeType.Departure;
            parameters.DesireTransiteTime = DateTime.Today.AddHours(3);
            parameters.TravelMode = TravelMode.Transit;

            var response = await service.CalculateRoutesAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.EqualTo(1));
            Assert.That(response.ResourceSets[0].Resources.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets[0].Resources.All(r => r is Route), Is.EqualTo(true));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Routes"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0").Value, Is.EqualTo(goldenGateBridge.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1").Value, Is.EqualTo(fishermansWharf.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "dt"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "dt").Value, Is.EqualTo(parameters.DesireTransiteTime.Value.ToString(BingRoutes.DateFormatMMddyyyy_HHmmss)));
            Assert.That(request.Parameters.Find(x => x.Name == "tt"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "tt").Value, Is.EqualTo(TransiteTimeType.Departure.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode").Value, Is.EqualTo(TravelMode.Transit.Key));
        }
        
        [Test]
        public async Task CalculateRoutesAsync_DrivingRouteWithRoutePath_ValidRoutes()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingRoutes>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var minneapolis = GeoAddress.CreateAddress("Minneapolis", "MN");
            var stPaul = GeoAddress.CreateAddress("St. Paul", "MN");
            var parameters = new CalculateRoutesParameters();
            parameters.TravelMode = TravelMode.Driving;
            parameters.WayPoints = new[] { minneapolis, stPaul };
            parameters.RouteOptimization = RouteOptimization.Distance;
            parameters.RouteAttributes = new [] { RouteAttribute.RoutePath };

            var response = await service.CalculateRoutesAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.EqualTo(1));
            Assert.That(response.ResourceSets[0].Resources.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets[0].Resources.All(r => r is Route), Is.EqualTo(true));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Routes"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0").Value, Is.EqualTo(minneapolis.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1").Value, Is.EqualTo(stPaul.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode").Value, Is.EqualTo(TravelMode.Driving.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "optmz"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "optmz").Value, Is.EqualTo(RouteOptimization.Distance.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "ra"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "ra").Value, Is.EqualTo(RouteAttribute.RoutePath.Key));
        }
        
        [Test]
        public async Task CalculateRoutesAsync_DrivingRouteUsingTolerances_ValidRoutes()
        {
            IRestRequest request = null;
            var serviceMock = new Mock<BingRoutes>();
            serviceMock.Setup(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => request = r)
                .CallBase();
            var service = serviceMock.Object;
            var wp1 = new GeoPoint(44.979035, -93.26493);
            var wp2 = new GeoPoint(44.943828508257866, -93.09332862496376);
            var parameters = new CalculateRoutesParameters();
            parameters.TravelMode = TravelMode.Driving;
            parameters.WayPoints = new[] { wp1, wp2 };
            parameters.RouteOptimization = RouteOptimization.Distance;
            parameters.Tolerances = new[] { 0.00000344978, 0.0000218840, 0.000220577, 0.00188803, 0.0169860, 0.0950130, 0.846703 };
            parameters.RouteAttributes = new[] { RouteAttribute.RoutePath };

            var response = await service.CalculateRoutesAsync(parameters);

            serviceMock.Verify(zc => zc.ExecuteAsync<Response>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.ResourceSets.Length, Is.EqualTo(1));
            Assert.That(response.ResourceSets[0].Resources.Length, Is.GreaterThan(0));
            Assert.That(response.ResourceSets[0].Resources.All(r => r is Route), Is.EqualTo(true));
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Method, Is.EqualTo(Method.GET));
            Assert.That(request.Resource, Is.EqualTo("Routes"));
            Assert.That(request.Parameters.Find(x => x.Name == "version"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "key"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "o"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "c"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.0").Value, Is.EqualTo(wp1.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "wp.1").Value, Is.EqualTo(wp2.GetFormattedString()));
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "travelMode").Value, Is.EqualTo(TravelMode.Driving.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "optmz"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "optmz").Value, Is.EqualTo(RouteOptimization.Distance.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "ra"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "ra").Value, Is.EqualTo(RouteAttribute.RoutePath.Key));
            Assert.That(request.Parameters.Find(x => x.Name == "tl"), Is.Not.Null);
            Assert.That(request.Parameters.Find(x => x.Name == "tl").Value, Is.EqualTo(string.Join(",", parameters.Tolerances.Select(p => p.ToString(CultureInfo.InvariantCulture)).ToArray())));
        }
    }
}
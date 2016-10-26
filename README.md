# Bing REST Services .NET
> Bing REST Services .NET library provides API for [Bing Maps REST Services](https://msdn.microsoft.com/en-us/library/ff701713.aspx) like [Routes](https://msdn.microsoft.com/en-us/library/ff701705.aspx), [Locations](https://msdn.microsoft.com/en-us/library/ff701715.aspx), [Traffic](https://msdn.microsoft.com/en-us/library/hh441725.aspx) and other Bing Maps REST services to perform tasks such as geocoding an address, creating a route, etc.

[![Build Status](https://travis-ci.org/ivanovvitaly/BingRestServices.Net.svg?branch=master)](https://travis-ci.org/ivanovvitaly/BingRestServices.Net)
[![NuGet](https://img.shields.io/nuget/v/BingRestServices.svg)](https://www.nuget.org/packages/BingRestServices)

## Installation

To install via NuGet run the following command in the Package Manager Console
```
Install-Package BingRestServices
```

## Examples 
>Your should have valid Bing API Key to use Bing REST Services

### Find location by Address (US)

```csharp
var parameters = new FindLocationByAddressParameters();
parameters.Address = GeoAddress.CreateAddress(
                "1 Microsoft Way",
                "Redmond",
                "WA",
                "98052",
                "US");

var bingLocations = new BingLocations(new BingConfiguration("API_KEY"));
var response = await bingLocations.FindLocationAsync(parameters);
var location = response.ResourceSets.First().Resources.OfType<Location>().First();

```

### Find location by Address (France)

```csharp
var parameters = new FindLocationByAddressParameters();
parameters.Address = new GeoAddress();
parameters.Address.CountryRegion = "FR";
parameters.Address.PostalCode = "75007";
parameters.Address.Locality = "Paris";
parameters.Address.AddressLine = "Avenue Gustave Eiffel";

var bingLocations = new BingLocations(new BingConfiguration("API_KEY"));
var response = await bingLocations.FindLocationAsync(parameters);
var location = response.ResourceSets.First().Resources.OfType<Location>().First();
```

### Find location by Point

```csharp
var parameters = new FindLocationByPointParameters();
parameters.Point = GeoPoint.Create(47.64054, -122.12934);

var bingLocations = new BingLocations(new BingConfiguration("API_KEY"));
var response = await bingLocations.FindLocationAsync(parameters);
var location = response.ResourceSets.First().Resources.OfType<Location>().First();
// location.Name: "Microsoft Way, Redmond, WA 98052"
```

### Find location by Query

```csharp
var parameters = new FindLocationByQueryParameters();
parameters.Query = GeoAddress.CreateLandmark("Eiffel Tower"); 

var bingLocations = new BingLocations(new BingConfiguration("API_KEY"));
var response = await bingLocations.FindLocationAsync(parameters);
var eiffelTower = response.ResourceSets.First().Resources.OfType<Location>().First();
```

### Find location including neighborhoods by Query

```csharp
var parameters = new FindLocationByQueryParameters();
parameters.IncludeNeighborhood = IncludeNeighborhood.Include;
parameters.Query = GeoAddress.CreateLandmark("Brookyln New York"); // with misprint

var bingLocations = new BingLocations(new BingConfiguration("API_KEY"));
var response = await bingLocations.FindLocationAsync(parameters);
var locations = response.ResourceSets.First().Resources.OfType<Location>();
var brooklyn = locations.First(p => p.EntityType == "PopulatedPlace");
var neighborhoods = locations.Where(p => p.EntityType == "Neighborhood");
```

### Calculate travel distance from DC to NY

```csharp
var parameters = new CalculateRoutesParameters();
parameters.TravelMode = TravelMode.Driving;
parameters.WayPoints = new IGeoLocation[] { GeoPoint.Create(38.890366, -77.031955), GeoPoint.Create(40.714545,  -74.007139) };

var bingRoutes = new BingRoutes(new BingConfiguration("API_KEY"));
var response = await bingRoutes.CalculateRoutesAsync(parameters);
var route = response.ResourceSets.First().Resources.OfType<Route>().First();
var travelDistance = route.TravelDistance;
...
```

### Calculate fastest route from Eiffel Tower to Louvre Museum
```csharp
var parameters = new CalculateRoutesParameters();
parameters.TravelMode = TravelMode.Walking;
parameters.RouteOptimization = RouteOptimization.Distance;
parameters.MaxSolutions = MaxSolutions.One;
parameters.WayPoints = new IGeoLocation[] { GeoAddress.CreateLandmark("Eiffel Tower"), GeoAddress.CreateLandmark("louvre museum") };

var bingRoutes = new BingRoutes(new BingConfiguration("API_KEY"));
var response = await bingRoutes.CalculateRoutesAsync(parameters);
var route = response.ResourceSets.First().Resources.OfType<Route>().First();
var travelDuration = route.TravelDuration;
...
```


### Calculate routes from Golden Gate Bridge to Fishermans Wharf
```csharp
var parameters = new CalculateRoutesParameters();
parameters.TravelMode = TravelMode.Transit;
parameters.TransiteTimeType = TransiteTimeType.Departure;
parameters.DesireTransiteTime = DateTime.Today.AddHours(3);
parameters.WayPoints = new IGeoLocation[] { GeoAddress.CreateLandmark("Golden Gate Bridge"), GeoAddress.CreateLandmark("Fishermans Wharf") };

var bingRoutes = new BingRoutes(new BingConfiguration("API_KEY"));
var response = await bingRoutes.CalculateRoutesAsync(parameters);
var routes = response.ResourceSets.First().Resources.OfType<Route>();
...
```

### Get all traffic incidents in a specified area
```csharp
var mapArea = new MapArea(37, -105, 45, -94);
var parameters = new TrafficIncidentsParameters(mapArea);

var bingTraffic = new BingTraffic(new BingConfiguration("API_KEY"));
var response = await bingTraffic.GetTrafficIncidents(parameters);
var trafficIncidents = response.ResourceSets.First().Resources.OfType<TrafficIncident>();
```

### Get traffic incidents by type and severity and request traffic location codes
```csharp
var mapArea = new MapArea(37, -105, 45, -94);
var parameters = new TrafficIncidentsParameters(mapArea);
parameters.IncludeLocationCodes = true;
parameters.Severity = new[] { Severity.Minor, Severity.Moderate };
parameters.TrafficIncidentTypes = new[] { TrafficIncidentType.Construction };

var bingTraffic = new BingTraffic(new BingConfiguration("API_KEY"));
var response = await bingTraffic.GetTrafficIncidents(parameters);
var trafficIncidents = response.ResourceSets.First().Resources.OfType<TrafficIncident>();
```

### Advanced configuration using code

```csharp
var jsonConfiguration = BingConfiguration.CreateJsonOutputConfiguration("API_KEY");
var bingRoutes = new BingRoutes(jsonConfiguration);
```

```csharp
var configuration = new BingConfiguration("API_KEY");
configuration.OutputFormat = "xml";
configuration.ErrorDetail = true;
configuration.Culture = "en-US";
...
var bingRoutes = new BingRoutes(configuration);
```

Configuration parameters description can be found here [Common Parameters and Types](https://msdn.microsoft.com/en-us/library/ff701720.aspx) and here [Bing Maps REST URL Structure](https://msdn.microsoft.com/en-us/library/ff701720.aspx). All parameters can be configuration through BingConfiguration class. 

### Advanced configuration using configuration section in .config file

```xml
<configuration>
  <configSections>
    <section name="bingConfiguration" type="BingRestServices.Configuration.BingConfigurationSection, BingRestServices" />
  </configSections>
  ...
  <bingConfiguration key="API_KEY"
                     baseUrl="http://dev.virtualearth.net/REST/v1/"
                     output="json">
  </bingConfiguration>
  ...
<configuration>
```
This way service can be instantiated without configuration object
```csharp
var bingRoutes = new BingRoutes();
await bingRoutes.CalculateRoutesAsync(...)
```

### Using service with DI (Ninject example)

With configuration section in .config file

```csharp
kernel.Bind<IBingRoutes>().To<BingRoutes>().InRequestScope();
```

Without configuration section

```csharp
kernel.Bind<IBingRoutes>().To<BingRoutes>()
    .WithConstructorArgument("configuration", _ => new BingConfiguration("API_KEY"))
    .InRequestScope();
```

## Development setup

1. Open the solution in Visual Studio 2013 and start a build.
2. Automatic restore should download and install each dependency package.

## Unit Tests

I use [NUnit](http://www.nunit.org/) and [Moq](https://github.com/moq/moq4) for Unit Tests.
In order test Bing Services you should have Bing API Key.
Put the API Key into _bingConfiguration_ section in the App.config file _BingRestServices.Tests_ project
```xml
<bingConfiguration key="API_KEY"
     baseUrl="http://dev.virtualearth.net/REST/v1/"
     output="json"
     culture="en-US">
```
Run unit tests from Visual Studio or using [nunitlite-runner](https://github.com/nunit/docs/wiki/NUnitLite-Runner) 

## Frameworks used in the project

- .NET 4.5 / C#
- [RestSharp](https://github.com/restsharp/RestSharp)
- [NUnit](http://www.nunit.org/)
- [Moq](https://github.com/moq/moq4)

## Release History

* 1.0.0
    * Introduced BingRoutes service for [Routes API](https://msdn.microsoft.com/en-us/library/ff701705.aspx)
* 1.1.0
    * Introduced BingLocations service for [Locations API](https://msdn.microsoft.com/en-us/library/ff701715.aspx)
* 1.2.0
    * Introduced BingTraffic service for [Traffic API](https://msdn.microsoft.com/en-us/library/hh441725.aspx)

## TODO

- [x] Implement Routes API
- [x] Implement Locations API
- [ ] Implement User Context Parameters
- [x] Implement Traffic API
- [ ] Implement Elevations API
- [ ] Implement Imagery API

## About Me

Vitaly Ivanov – [GitHub](https://github.com/ivanovvitaly) - [Blog](http://delmadman.blogspot.com/) - [StackOverlow](http://stackoverflow.com/users/344895/madman) – ivanov.vitalii@gmail.com

## License 
This project is licensed under the terms of the MIT license.
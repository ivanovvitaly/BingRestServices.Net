# Bing REST Services .NET
> Bing REST Services .NET library provides API for [Bing Maps REST Services](https://msdn.microsoft.com/en-us/library/ff701713.aspx) like [Routes](https://msdn.microsoft.com/en-us/library/ff701705.aspx), [Locations](https://msdn.microsoft.com/en-us/library/ff701715.aspx), [Traffic](https://msdn.microsoft.com/en-us/library/hh441725.aspx) and other Bing Maps REST services to perform tasks such as geocoding an address, creating a route, etc.

## Installation

To install via NuGet run the following command in the Package Manager Console
```
Install-Package BingRestServices
```

## Examples 

>Your should have valid Bing API Key to use Bing REST Services

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

## TODO

- [x] Implement Routes API
- [ ] Implement Locations API
- [ ] Implement Traffic API
- [ ] Elevations API
- [ ] Implement Imagery API

## About Me

Vitaly Ivanov – [GitHub](https://github.com/ivanovvitaly) - [Blog](http://delmadman.blogspot.com/) - [StackOverlow](http://stackoverflow.com/users/344895/madman) – ivanov.vitalii@gmail.com

## License 
This project is licensed under the terms of the MIT license.
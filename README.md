![.NET build and test](https://github.com/kwtc/error-monitoring-client-dotnet/actions/workflows/ci.yml/badge.svg)

# [WIP] Error monitoring - dotnet client

> **Warning**
> This is work in progress

Dotnet client for posting requests to the [Kwtc.ErrorMonitoring](https://github.com/kwtc/error-monitoring) api.

## Features
- [X] Extract info from exception, serialize and send as report
- [ ] Limit report size
- [X] ASP.NET Core Web API middleware

## Configuration
A section is required in `appsettings.json` defining an api key and an application id both formatted as a GUID and a URL pointing to the running instance of [Kwtc.ErrorMonitoring](https://github.com/kwtc/kwtc-error-monitoring).
Optionally a dotnet HttpClient name and a exception event capasity (defaulting to 1000) can also be defined. 

See the following example:

```json
"ErrorMonitoring": {
    "ApiKey": "6d4780de-2a6e-4b43-9595-4afd592407e3",
    "ApplicationKey": "a8d72dfb-5c97-41a0-961d-b951de367031",
    "EndpointUri": "http://localhost:5000/api/v1/",
    "HttpClientName": "ErrorMonitoringClient",
    "ChannelCapasity": 1000
}
```

## Middleware
To use the ASP.NET middleware simply register the required services and add error monitoring to your application.

```c#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddErrorMonitoring();

var app = builder.Build();
app.UseErrorMonitoring();

app.Run();
```


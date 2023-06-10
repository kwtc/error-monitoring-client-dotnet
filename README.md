![example workflow](https://github.com/kwtc/error-monitoring-client-dotnet/actions/workflows/dotnet.yml/badge.svg)

# [WIP] Error monitoring - dotnet client
Dotnet client implementation for posting requests to the [Kwtc.ErrorMonitoring](https://github.com/kwtc/kwtc-error-monitoring) api.

## Features
- [X] Extract info from exception, serialize and send as report
- [ ] Limit report size
- [ ] ASP.NET Core Web API middleware

## Configuration
A section is required in `appsettings.json` defining an api key and an application id both formatted as a GUID and a URL pointing to the running instance of [Kwtc.ErrorMonitoring](https://github.com/kwtc/kwtc-error-monitoring). See the following example:

```json
"ErrorMonitoring": {
    "ApiKey": "6d4780de-2a6e-4b43-9595-4afd592407e3",
    "ApplicationId": "a8d72dfb-5c97-41a0-961d-b951de367031",
    "Endpoint": "http://localhost:5000/api/v1/"
}
```


# [WIP] Error monitoring - dotnet client
Dotnet client implementation for posting requests to the [Kwtc.ErrorMonitoring](https://github.com/kwtc/kwtc-error-monitoring) api.

## WORK IN PROGRESS!
- [X] Extract info from exception, serialize and send as report
- [ ] Limit report size
- [ ] ASP.NET Core Web API middleware

## Configuration
A section is required in `appsettings.json` defining an api key formatted as a GUID and a URL pointing to the running instance of [Kwtc.ErrorMonitoring](https://github.com/kwtc/kwtc-error-monitoring). See the following example:

```json
"ErrorMonitoring": {
    "ApiKey": "6d4780de-2a6e-4b43-9595-4afd592407e3",
    "AppIdentifier": "Test app",
    "Url": "http://localhost:5114"
}
```


namespace Kwtc.ErrorMonitoring.Client;

using System.Text.Json;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Payload;

public class ApiClient : IApiClient
{
    private readonly string? apiKey;
    private readonly string? applicationId;
    private readonly string? endpoint;

    public ApiClient(IConfiguration configuration)
    {
        const string configurationMissingExceptionMessage =
            "is not set in configuration or is invalid. See https://github.com/kwtc/kwtc-error-monitoring-dotnet-client for configuration details.";

        this.apiKey = configuration[ConfigurationKeys.ApiKey];
        if (string.IsNullOrEmpty(this.apiKey))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.ApiKey} {configurationMissingExceptionMessage}");
        }

        this.applicationId = configuration[ConfigurationKeys.ApplicationId];
        if (string.IsNullOrEmpty(applicationId))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.ApplicationId} {configurationMissingExceptionMessage}");
        }

        this.endpoint = configuration[ConfigurationKeys.Endpoint];
        if (string.IsNullOrEmpty(this.endpoint))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.Endpoint} {configurationMissingExceptionMessage}");
        }
    }

    public async Task NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default)
    {
        var errorEvent = new Event(exception, severity, this.applicationId!, isHandled);
        var payload = JsonSerializer.Serialize(errorEvent);
        await $"{this.endpoint}/events"
              .WithHeader("x-api-key", this.apiKey)
              .PostJsonAsync(payload, cancellationToken);
    }
}

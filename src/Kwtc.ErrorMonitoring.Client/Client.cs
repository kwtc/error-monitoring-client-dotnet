using System.Text.Json;
using Flurl.Http;
using Kwtc.ErrorMonitoring.Client.Payload;
using Microsoft.Extensions.Configuration;

namespace Kwtc.ErrorMonitoring.Client;

public class Client : IClient
{
    private readonly string? apiKey;
    private readonly string? applicationId;
    private readonly string? endpoint;

    public Client(IConfiguration configuration)
    {
        const string configurationMissingExceptionMessage =
            $"is not set in configuration or is invalid. See {Constants.ProjectSite} for configuration details.";

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

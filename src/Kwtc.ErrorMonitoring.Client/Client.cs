using Flurl.Http;
using Kwtc.ErrorMonitoring.Client.Payload;
using Microsoft.Extensions.Configuration;

namespace Kwtc.ErrorMonitoring.Client;

public class Client : IClient
{
    private readonly string? apiKey;
    private readonly string? applicationKey;
    private readonly string? endpointUri;

    public Client(IConfiguration configuration)
    {
        const string configurationMissingExceptionMessage =
            $"is not set in configuration or is invalid. See {Constants.ProjectSite} for configuration details.";

        this.apiKey = configuration[ConfigurationKeys.ApiKey];
        if (string.IsNullOrEmpty(this.apiKey) || !Guid.TryParse(this.apiKey, out _))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.ApiKey} {configurationMissingExceptionMessage}");
        }

        this.applicationKey = configuration[ConfigurationKeys.ApplicationKey];
        if (string.IsNullOrEmpty(this.applicationKey) || !Guid.TryParse(this.applicationKey, out _))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.ApplicationKey} {configurationMissingExceptionMessage}");
        }

        this.endpointUri = configuration[ConfigurationKeys.EndpointUri];
        if (string.IsNullOrEmpty(this.endpointUri) || !Uri.IsWellFormedUriString(this.endpointUri, UriKind.Absolute))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.EndpointUri} {configurationMissingExceptionMessage}");
        }
    }

    public async Task NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default)
    {
        var errorEvent = new Event(exception, severity, this.applicationKey!, isHandled);
        await $"{this.endpointUri}/events"
              .WithHeader("x-api-key", this.apiKey)
              .PostJsonAsync(errorEvent, cancellationToken: cancellationToken);
    }
}

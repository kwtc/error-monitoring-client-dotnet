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
        this.apiKey = configuration[ConfigurationKeys.ApiKey];
        this.applicationKey = configuration[ConfigurationKeys.ApplicationKey];
        this.endpointUri = configuration[ConfigurationKeys.EndpointUri] + "/events";

        this.ValidateConfiguration();
    }

    public async Task<HttpResponseMessage> NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default)
    {
        var errorEvent = new Event(exception, severity, this.applicationKey!, isHandled);

        try
        {
            return (await this.endpointUri
                              .WithHeader("x-api-key", this.apiKey)
                              .PostJsonAsync(errorEvent, cancellationToken: cancellationToken)).ResponseMessage;
        }
        catch (FlurlHttpException ex)
        {
            throw new ErrorMonitoringException(ex.Message, ex);
        }
    }

    private void ValidateConfiguration()
    {
        const string configurationMissingExceptionMessage =
            $"is not set in configuration or is invalid. See {Constants.ProjectSite} for configuration details.";

        if (string.IsNullOrEmpty(this.apiKey) || !Guid.TryParse(this.apiKey, out _))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.ApiKey} {configurationMissingExceptionMessage}");
        }

        if (string.IsNullOrEmpty(this.applicationKey) || !Guid.TryParse(this.applicationKey, out _))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.ApplicationKey} {configurationMissingExceptionMessage}");
        }

        if (string.IsNullOrEmpty(this.endpointUri) || !Uri.IsWellFormedUriString(this.endpointUri, UriKind.Absolute))
        {
            throw new ErrorMonitoringException($"{ConfigurationKeys.EndpointUri} {configurationMissingExceptionMessage}");
        }
    }
}

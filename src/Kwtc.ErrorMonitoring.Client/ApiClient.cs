namespace Kwtc.ErrorMonitoring.Client;

using System.Text.Json;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Payload;

public class ApiClient : IApiClient
{
    private readonly string? apiKey;
    private readonly string? appIdentifier;
    private readonly string? url;

    public ApiClient(IConfiguration configuration)
    {
        this.apiKey = configuration["ErrorMonitoring:ApiKey"];
        if (string.IsNullOrEmpty(this.apiKey))
        {
            throw new ErrorMonitoringException(
                "Kwtc.ErrorMonitoring.ApiKey is not set in configuration. See https://github.com/kwtc/kwtc-error-monitoring-dotnet-client for configuration details.");
        }

        this.appIdentifier = configuration["ErrorMonitoring:AppIdentifier"];
        if (string.IsNullOrEmpty(this.appIdentifier))
        {
            throw new ErrorMonitoringException(
                "Kwtc.ErrorMonitoring.AppIdentifier is not set in configuration. See https://github.com/kwtc/kwtc-error-monitoring-dotnet-client for configuration details.");
        }

        this.url = configuration["ErrorMonitoring:Url"];
        if (string.IsNullOrEmpty(this.url))
        {
            throw new ErrorMonitoringException(
                "Kwtc.ErrorMonitoring.Url is not set in configuration. See https://github.com/kwtc/kwtc-error-monitoring-dotnet-client for configuration details.");
        }
    }

    public async Task NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default)
    {
        var errorEvent = new Event(exception, severity, this.appIdentifier!, isHandled);
        var payload = JsonSerializer.Serialize(new Report(errorEvent));
        await $"{this.url}/report/notify"
              .WithHeader("x-api-key", this.apiKey)
              .PostJsonAsync(payload, cancellationToken);
    }
}

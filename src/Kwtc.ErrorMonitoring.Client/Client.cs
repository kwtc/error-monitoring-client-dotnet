namespace Kwtc.ErrorMonitoring.Client;

using System.Text.Json;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Payload;

public class Client : IClient
{
    private readonly string? apiKey;
    private readonly string? url;

    public Client(IConfiguration configuration)
    {
        this.apiKey = configuration["ErrorMonitoring:ApiKey"];
        if (string.IsNullOrEmpty(this.apiKey))
        {
            throw new ErrorMonitoringException(
                "Kwtc.ErrorMonitoring.ApiKey is not set in configuration. See https://github.com/kwtc/kwtc-error-monitoring-dotnet-client for configuration details.");
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
        // TODO: Create wrapper object for api key etc.

        var errorEvent = new Event(exception, severity, isHandled);
        var payload = JsonSerializer.Serialize(new Report(Guid.NewGuid(), errorEvent));
        await $"{this.url}/report/notify".PostJsonAsync(payload, cancellationToken);
    }
}

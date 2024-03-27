using System.Net.Http.Json;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using Kwtc.ErrorMonitoring.Client.Payload;
using Kwtc.ErrorMonitoring.Client.Validation;
using Microsoft.Extensions.Configuration;
using Severity = Kwtc.ErrorMonitoring.Client.Payload.Severity;

namespace Kwtc.ErrorMonitoring.Client;

public class Client : IClient
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly string applicationKey;
    private readonly string endpointUri;
    private readonly string httpClientName;

    public Client(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;

        new ClientConfigurationValidator().ValidateAndThrow(configuration);

        this.applicationKey = configuration[ConfigurationKeys.ApplicationKey]!;
        this.endpointUri = configuration[ConfigurationKeys.EndpointUri]!;
        this.httpClientName = configuration[ConfigurationKeys.HttpClientName] ?? Constants.DefaultHttpClientName;
    }

    public async Task<ClientResponse> NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(exception, nameof(exception));
        Guard.IsGreaterThan((int)severity, 1, nameof(severity));

        var errorEvent = new Event(exception, severity, this.applicationKey, isHandled);
        var client = this.httpClientFactory.CreateClient(this.httpClientName);
        var content = JsonContent.Create(errorEvent);

        try
        {
            var response = await client.PostAsync($"{this.endpointUri}/events", content, cancellationToken);
            return new ClientResponse((int)response.StatusCode, response.ReasonPhrase);
        }
        catch (System.Exception ex)
        {
            throw new ErrorMonitoringException(ex.Message, ex);
        }
    }
}

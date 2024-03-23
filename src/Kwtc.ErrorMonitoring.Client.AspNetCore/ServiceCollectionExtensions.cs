using System.Threading.Channels;
using FluentValidation;
using Kwtc.ErrorMonitoring.Client.AspNetCore.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorMonitoring(this IServiceCollection services, IConfiguration configuration)
    {
        new ErrorMonitoringConfigurationValidator().ValidateAndThrow(configuration);

        if (!int.TryParse(configuration[ConfigurationKeys.ChannelCapasity], out var capasity))
        {
            capasity = 1000;
        }

        services
            .AddTransient<IClient, Client>()
            .AddSingleton(Channel.CreateBounded<ExceptionEvent>(capasity))
            .AddHostedService<ExceptionEventBackgroundService>()
            .AddHttpClient(configuration[ConfigurationKeys.HttpClientName] ?? Constants.DefaultHttpClientName, httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration[ConfigurationKeys.EndpointUri]!);
                httpClient.DefaultRequestHeaders.Add(Constants.HeaderNameApiKey, configuration[ConfigurationKeys.ApiKey]!);
            });

        return services;
    }
}

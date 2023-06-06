namespace Kwtc.ErrorMonitoring.AspNetCore;

using Client;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddErrorMonitoring(this IServiceCollection services)
    {
        // TODO: Add the ErrorMonitoringMiddleware to the pipeline

        services.AddScoped<IApiClient, ApiClient>();

        return services;
    }
}

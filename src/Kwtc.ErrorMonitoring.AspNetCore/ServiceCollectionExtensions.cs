namespace Kwtc.ErrorMonitoring.AspNetCore;

using Client;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorMonitoring(this IServiceCollection services)
    {
        services.AddScoped<IApiClient, ApiClient>();

        return services;
    }
}

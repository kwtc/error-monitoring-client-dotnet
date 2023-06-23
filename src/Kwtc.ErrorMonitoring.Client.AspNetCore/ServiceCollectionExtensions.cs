using Microsoft.Extensions.DependencyInjection;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorMonitoring(this IServiceCollection services)
    {
        services.AddScoped<IApiClient, ApiClient>();

        return services;
    }
}

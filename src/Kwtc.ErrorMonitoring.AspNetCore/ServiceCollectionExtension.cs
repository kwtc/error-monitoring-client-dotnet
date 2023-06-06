namespace Kwtc.ErrorMonitoring.AspNetCore;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddErrorMonitoring(this IServiceCollection services)
    {
        // TODO: Add the ErrorMonitoringMiddleware to the pipeline

        return services;
    }
}

using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorMonitoring(this IServiceCollection services)
    {
        services
            .AddScoped<IClient, Client>()
            .AddSingleton(Channel.CreateBounded<ExceptionEvent>(10000));

        return services;
    }
}

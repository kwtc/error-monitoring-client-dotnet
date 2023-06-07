namespace Kwtc.ErrorMonitoring.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Middleware;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorMonitoring(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorMonitoringMiddleware>();

        return app;
    }
}

using Microsoft.AspNetCore.Builder;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorMonitoring(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorMonitoringMiddleware>();
    }
}

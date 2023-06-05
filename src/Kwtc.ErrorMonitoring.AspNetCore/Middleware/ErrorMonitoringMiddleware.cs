namespace Kwtc.ErrorMonitoring.AspNetCore.Middleware;

using System.Net;
using System.Threading.Channels;
using Microsoft.AspNetCore.Http;

public class ErrorMonitoringMiddleware
{
    private readonly RequestDelegate next;

    public ErrorMonitoringMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            // var channel = Channel.CreateUnbounded<>();

            throw;
        }
    }
}

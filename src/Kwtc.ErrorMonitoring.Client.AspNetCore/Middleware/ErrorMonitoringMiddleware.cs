using System.Threading.Channels;
using Microsoft.AspNetCore.Http;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore.Middleware;

public class ErrorMonitoringMiddleware
{
    private readonly RequestDelegate next;

    public ErrorMonitoringMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, Channel<ExceptionEvent> channel)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            channel.Writer.TryWrite(new ExceptionEvent(ex));

            throw;
        }
    }
}

using System.Threading.Channels;
using Microsoft.AspNetCore.Http;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public class ErrorMonitoringMiddleware
{
    private readonly RequestDelegate next;
    private readonly Channel<ExceptionEvent> channel;

    public ErrorMonitoringMiddleware(RequestDelegate next, Channel<ExceptionEvent> channel)
    {
        this.next = next;
        this.channel = channel;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            await this.channel.Writer.WriteAsync(new ExceptionEvent(ex));

            throw;
        }
    }
}

using System.Threading.Channels;
using Kwtc.ErrorMonitoring.Client.Payload;
using Microsoft.AspNetCore.Http;
using Exception = System.Exception;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public class ErrorMonitoringMiddleware
{
    private readonly RequestDelegate next;

    public ErrorMonitoringMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, Channel<ExceptionEvent> channel, IApiClient client)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            await channel.Writer.WriteAsync(new ExceptionEvent(ex));

            while (await channel.Reader.WaitToReadAsync())
            {
                while (channel.Reader.TryRead(out var exceptionEvent))
                {
                    await client.NotifyAsync(exceptionEvent.Exception, Severity.Error);
                }
            }

            throw;
        }
    }
}

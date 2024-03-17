using System.Threading.Channels;
using Kwtc.ErrorMonitoring.Client.Payload;
using Microsoft.Extensions.Hosting;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public class ExceptionEventBackgroundService : BackgroundService
{
    private readonly Channel<ExceptionEvent> channel;
    private readonly IClient client;

    public ExceptionEventBackgroundService(Channel<ExceptionEvent> channel, IClient client)
    {
        this.channel = channel;
        this.client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await this.channel.Reader.WaitToReadAsync(CancellationToken.None))
        {
            while (channel.Reader.TryRead(out var exceptionEvent))
            {
                await this.client.NotifyAsync(exceptionEvent.Exception, Severity.Error, cancellationToken: CancellationToken.None);
            }
        }
    }
}

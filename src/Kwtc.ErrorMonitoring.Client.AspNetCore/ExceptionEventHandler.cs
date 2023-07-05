using System.Threading.Channels;
using Microsoft.Extensions.Hosting;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public class ExceptionEventHandler : IHostedService
{
    private readonly Channel<ExceptionEvent> channel;

    public ExceptionEventHandler(Channel<ExceptionEvent> channel)
    {
        this.channel = channel;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

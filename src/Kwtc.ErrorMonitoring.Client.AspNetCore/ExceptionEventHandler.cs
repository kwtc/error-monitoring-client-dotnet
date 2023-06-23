using System.Threading.Channels;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public class ExceptionEventHandler
{
    private readonly Channel<ExceptionEvent> channel;

    public ExceptionEventHandler(Channel<ExceptionEvent> channel)
    {
        this.channel = channel;
    }
}

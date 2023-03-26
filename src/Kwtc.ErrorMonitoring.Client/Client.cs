namespace Kwtc.ErrorMonitoring.Client;

using Microsoft.Extensions.Configuration;
using Payload;

public class Client : IClient
{
    public Client(IConfiguration configuration)
    {
        
    }

    public Task NotifyAsync(System.Exception exception, Severity severity)
    {
        throw new NotImplementedException();
    }
}

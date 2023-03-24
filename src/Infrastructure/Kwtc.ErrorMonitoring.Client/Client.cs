namespace Kwtc.ErrorMonitoring.Client;

using Abstractions;
using Microsoft.Extensions.Configuration;

public class Client : IClient
{
    public Client(IConfiguration configuration)
    {
        
    }

    public Task NotifyAsync(Exception exception, Severity severity)
    {
        throw new NotImplementedException();
    }
}

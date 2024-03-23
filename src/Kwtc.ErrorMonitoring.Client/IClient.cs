using Kwtc.ErrorMonitoring.Client.Payload;

namespace Kwtc.ErrorMonitoring.Client;

public interface IClient
{
    /// <summary>
    /// Sends an exception event  
    /// </summary>
    Task<ClientResponse> NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default);
}

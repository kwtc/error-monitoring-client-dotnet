using Kwtc.ErrorMonitoring.Client.Payload;

namespace Kwtc.ErrorMonitoring.Client;

public interface IClient
{
    Task<HttpResponseMessage> NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default);
}

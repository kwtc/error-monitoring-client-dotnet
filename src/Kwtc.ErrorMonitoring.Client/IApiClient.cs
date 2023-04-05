namespace Kwtc.ErrorMonitoring.Client;

using Payload;

public interface IApiClient
{
    Task NotifyAsync(System.Exception exception, Severity severity, bool isHandled = false, CancellationToken cancellationToken = default);
}

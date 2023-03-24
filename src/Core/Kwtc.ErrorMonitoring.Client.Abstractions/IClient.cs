namespace Kwtc.ErrorMonitoring.Client.Abstractions;

public interface IClient
{
    Task NotifyAsync(Exception exception, Severity severity);
}

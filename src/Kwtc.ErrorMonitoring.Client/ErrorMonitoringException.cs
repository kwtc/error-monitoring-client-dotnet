namespace Kwtc.ErrorMonitoring.Client;

public class ErrorMonitoringException : Exception
{
    public ErrorMonitoringException(string message) : base(message)
    {
    }
}

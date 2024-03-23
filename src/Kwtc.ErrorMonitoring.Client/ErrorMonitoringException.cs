namespace Kwtc.ErrorMonitoring.Client;

public class ErrorMonitoringException : Exception
{
    public ErrorMonitoringException(string message) : base(message)
    {
    }

    public ErrorMonitoringException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

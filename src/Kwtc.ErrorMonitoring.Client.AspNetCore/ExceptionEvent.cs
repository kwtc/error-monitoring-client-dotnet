namespace Kwtc.ErrorMonitoring.Client.AspNetCore;

public class ExceptionEvent
{
    public Exception Exception { get; }

    public ExceptionEvent(Exception exception)
    {
        this.Exception = exception;
    }
}

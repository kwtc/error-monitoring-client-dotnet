namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Event : Dictionary<string, object>
{
    public Event(System.Exception exception, Severity severity, string appIdentifier, bool isHandled = false)
    {
        this.Add("appIdentifier", appIdentifier);
        this.Add("exceptionType", exception.GetType().ToString());
        this.Add("exceptionMessage", exception.Message);
        this.Add("severity", severity.GetDescription().ToLower());
        this.Add("isHandled", isHandled);
        this.Add("exceptions", new ExceptionCollection(exception));
    }
}

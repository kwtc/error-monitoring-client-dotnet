namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Event : Dictionary<string, object>
{
    public Event(System.Exception exception, Severity severity, string applicationId, bool isHandled = false)
    {
        this.Add("applicationId", applicationId);
        this.Add("exceptionType", exception.GetType().ToString());
        this.Add("exceptions", new ExceptionCollection(exception));
        this.Add("severity", severity.GetDescription().ToLower());
        this.Add("isHandled", isHandled);
        this.Add("metadata", new Metadata());
    }
}

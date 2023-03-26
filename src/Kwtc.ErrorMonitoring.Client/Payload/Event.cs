namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Event : Dictionary<string, object>
{
    public Event(System.Exception exception, Severity severity, bool isHandled = false)
    {
        this.Add("exceptions", new ExceptionCollection(exception));
        this.Add("severity", severity.GetDescription().ToLower());
        this.Add("isHandled", isHandled);
    }
}

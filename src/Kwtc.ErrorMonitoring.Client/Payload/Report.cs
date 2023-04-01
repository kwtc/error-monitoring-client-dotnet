namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Report : Dictionary<string, object>
{
    public Report(Event @event)
    {
        this.Add("event", @event);
    }
}

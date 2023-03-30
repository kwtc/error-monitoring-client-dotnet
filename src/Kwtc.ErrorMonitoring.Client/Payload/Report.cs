namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Report : Dictionary<string, object>
{
    public Report(Guid apiKey, Event @event)
    {
        this.Add("apiKey", apiKey.ToString());
        this.Add("event", @event);
    }
}

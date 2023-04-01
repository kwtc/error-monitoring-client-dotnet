namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Exception : Dictionary<string, object>
{
    public Exception(System.Exception exception)
    {
        this.Add("type", exception.GetType().ToString());
        this.Add("message", exception.Message);
        this.Add("trace", new Trace(exception).ToArray());
    }
}

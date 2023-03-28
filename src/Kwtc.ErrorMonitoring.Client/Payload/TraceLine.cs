namespace Kwtc.ErrorMonitoring.Client.Payload;

public class TraceLine : Dictionary<string, object>
{
    public TraceLine(string file, int lineNumber, string method)
    {
        this.Add("file", file);
        this.Add("lineNumber", lineNumber);
        this.Add("method", method);
    }
}

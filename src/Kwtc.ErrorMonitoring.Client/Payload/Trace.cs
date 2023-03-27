namespace Kwtc.ErrorMonitoring.Client.Payload;

using System.Collections;

public class Trace : IEnumerable<TraceLine>
{
    private readonly IEnumerable<TraceLine> traceLines;

    public Trace(System.Exception exception)
    {
        
    }
    
    public IEnumerator<TraceLine> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

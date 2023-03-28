namespace Kwtc.ErrorMonitoring.Client.Payload;

using System.Collections;
using System.Diagnostics;

public class Trace : IEnumerable<TraceLine>
{
    private readonly System.Exception exception;

    public Trace(System.Exception exception)
    {
        this.exception = exception;
    }

    public IEnumerator<TraceLine> GetEnumerator()
    {
        if (this.exception == null)
        {
            yield break;
        }

        var frames = new StackTrace(this.exception, true).GetFrames();
        if (frames == null)
        {
            yield break;
        }

        foreach (var frame in frames)
        {
            yield return new TraceLine(
                frame.GetFileName() ?? string.Empty,
                frame.GetFileLineNumber(),
                frame.GetMethod()?.Name ?? string.Empty
            );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

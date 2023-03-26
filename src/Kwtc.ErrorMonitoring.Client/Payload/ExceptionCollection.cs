namespace Kwtc.ErrorMonitoring.Client.Payload;

using System.Collections;

public class ExceptionCollection : IEnumerable<Exception>
{
    private readonly IEnumerable<Exception> exceptions;

    public ExceptionCollection(System.Exception exception)
    {
        this.exceptions = FlattenExceptionTree(exception).Select(e => new Exception(e));
    }

    public IEnumerator<Exception> GetEnumerator()
    {
        return this.exceptions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private static IEnumerable<System.Exception> FlattenExceptionTree(System.Exception exception)
    {
        var exceptions = new List<System.Exception> { exception };
        if (exception.InnerException != null)
        {
            exceptions.AddRange(FlattenExceptionTree(exception.InnerException));
        }

        return exceptions;
    }
}

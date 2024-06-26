using System.Text;
using System.Text.Json;

namespace Kwtc.ErrorMonitoring.Client.Payload;

public class Report : Dictionary<string, object>
{
    private const int MaximumSize = 1024 * 1024;

    public Report(System.Exception exception)
    {
    }

    public Report(Event @event)
    {
        this.Add("event", @event);
    }

    public byte[] Serialize()
    {
        byte[] data;
        try
        {
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
            if (data.Length > MaximumSize)
            {
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return data;
    }

    private byte[] TrimData(byte[] data)
    {
        var trimmedData = new byte[MaximumSize];
        Array.Copy(data, trimmedData, MaximumSize);
        return trimmedData;
    }
}

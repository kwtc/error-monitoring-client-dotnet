namespace Kwtc.ErrorMonitoring.Client;

public class ClientResponse
{
    public int? StatusCode { get; init; }
    public string? Message { get; init; }

    public ClientResponse(int? statusCode, string? message)
    {
        this.StatusCode = statusCode;
        this.Message = message;
    }
}

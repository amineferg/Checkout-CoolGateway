namespace CoolGateway.Application.Common.Models;

public class BankResponse
{
    public BankResponse(Guid id, string status)
    {
        Id = id;
        Status = status;
    }

    public BankResponse(string status)
    {
        Status = status;
    }

    public Guid Id { get; }

    public string Status { get; }
}

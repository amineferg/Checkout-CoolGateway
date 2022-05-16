using CoolGateway.Application.Payments.Commands;

namespace CoolGateway.WebApi.Dtos;

public class ProcessPaymentDto
{
    public Guid MerchantId { get; set; }

    public string CardNumber { get; set; }

    public string CardHolder { get; set; }

    public int CardExpiryMonth { get; set; }

    public int CardExpiryYear { get; set; }

    public int CardCvv { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }
}

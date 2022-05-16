using CoolGateway.Application.Common.Models;

namespace CoolGateway.Infrastructure.Bank;

internal class CoolBank : IBank
{
    public BankResponse ProcessPayment(
        Guid merchantId,
        string cardNumber,
        string cardHolder,
        int cardExpiryMonth,
        int cardExpiryYear,
        int cvv,
        decimal amout,
        string currency)
    {
        // Simulated implementation.

        return currency == "COOL"
            ? new BankResponse(Guid.NewGuid(), "Success")
            : new BankResponse("Invalid payment method");
    }
}

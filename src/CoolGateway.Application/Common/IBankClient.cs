using CoolGateway.Application.Common.Models;

namespace CoolGateway.Application.Common;

public interface IBankClient
{
    BankResponse ProcessPayment(
        Guid merchantId,
        string cardNumber,
        string cardHolder,
        int cardExpiryMonth,
        int cardExpiryYear,
        int cvv,
        decimal amout,
        string currency);
}

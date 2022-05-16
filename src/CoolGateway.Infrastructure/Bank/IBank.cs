using CoolGateway.Application.Common.Models;

namespace CoolGateway.Infrastructure.Bank;

/// <summary>
/// Banks will implement this interface and provide their specific way to process the payment.
/// </summary>
internal interface IBank
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

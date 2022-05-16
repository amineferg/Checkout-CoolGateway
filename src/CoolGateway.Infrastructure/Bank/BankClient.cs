using CoolGateway.Application.Common;
using CoolGateway.Application.Common.Models;

namespace CoolGateway.Infrastructure.Bank;

internal class BankClient : IBankClient
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
        // Add validation and other steps.

        var bin = cardNumber.Substring(0, 6);

        var bank = BinResolver.Resolve(bin);
        if (bank == null)
        {
            return new BankResponse(Guid.Empty, "Unkown card issuer.");
        }

        return bank.ProcessPayment(merchantId, cardNumber, cardHolder, cardExpiryMonth, cardExpiryYear, cvv, amout, currency);
    }
}

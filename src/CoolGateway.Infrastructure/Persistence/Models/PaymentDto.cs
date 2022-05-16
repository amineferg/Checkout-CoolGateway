using CoolGateway.Domain.Payments;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolGateway.Infrastructure.Persistence.Models;

[Table("Payment")]
internal class PaymentDto : EntityDto<Payment>
{
    public Guid MerchantId { get; set; }

    public string CardNumber { get; set; }

    public string CardHolder { get; set; }

    public int CardExpiryMonth { get; set; }

    public int CardExpiryYear { get; set; }

    public int CardCvv { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public override void MapFrom(Payment source)
    {
        if (source == null)
        {
            return;
        }

        Id = source.Id;
        MerchantId = source.MerchantId;

        if (source.CardInformation != null)
        {
            CardNumber = source.CardInformation.Number;
            CardHolder = source.CardInformation.Holder;
            CardExpiryMonth = source.CardInformation.ExpiryMonth;
            CardExpiryYear = source.CardInformation.ExpiryYear;
            CardCvv = source.CardInformation.Cvv;
        }

        Amount = source.Amount.Value;
        Currency = source.Amount.Currency;
    }

    public override Payment MapTo()
    {
        var cardInformation = new CardInformation(CardNumber, CardHolder, CardExpiryMonth, CardExpiryYear, CardCvv);
        var paymentAmount = new PaymentAmount(Amount, Currency);
        return new Payment(Id, MerchantId, cardInformation, paymentAmount);
    }
}

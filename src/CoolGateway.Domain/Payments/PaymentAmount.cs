using CoolGateway.Domain.Common;

namespace CoolGateway.Domain.Payments;

/// <summary>
/// Money Pattern : @ https://martinfowler.com/eaaCatalog/money.html .
/// </summary>
public class PaymentAmount : ValueObject
{
    public PaymentAmount(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    public decimal Value { get; }

    public string Currency { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}

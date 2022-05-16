using CoolGateway.Domain.Common;
using CoolGateway.Domain.Payments.Exceptions;
using CoolGateway.Domain.Payments.Validators;

namespace CoolGateway.Domain.Payments;

public class CardInformation : ValueObject
{
    private const int NumberOfUnmaskedDigits = 4;

    public CardInformation(string number, string holder, int expiryMonth, int expiryYear, int cvv)
    {
        Number = number;
        Holder = holder;
        ExpiryMonth = expiryMonth;
        ExpiryYear = expiryYear;
        Cvv = cvv;
        SetMaskedCardNumber();

        Validate();
    }

    public CardInformation(string number, string holder, string expiryDate, int cvv)
    {
        Number = number;
        Holder = holder;
        Cvv = cvv;
        SetExpiryDate(expiryDate);
        SetMaskedCardNumber();

        Validate();
    }

    public string Number { get; }

    public string MaskedNumber { get; private set; } = string.Empty;

    public string Holder { get; }

    public int ExpiryMonth { get; private set; }

    public int ExpiryYear { get; private set; }

    public int Cvv { get; }

    private void Validate()
    {
        var validator = new CartInformationValidator();
        var validationResult = validator.Validate(this);
        if (!validationResult.IsValid)
        {
            throw new PaymentDomainValidationException(
                $"Invalid domain model {nameof(CardInformation)} : " +
                $"{string.Join(", ", validationResult.Errors.Select(e => $"{e.PropertyName} => {e.ErrorMessage}").ToArray())}");
        }
    }

    private void SetMaskedCardNumber()
    {
        if (string.IsNullOrEmpty(Number))
        {
            return;
        }

        try
        {
            var visible = new string(Number.Skip(Number.Length - NumberOfUnmaskedDigits).ToArray());
            var asterisks = new string('*', Number.Length - NumberOfUnmaskedDigits);
            MaskedNumber = asterisks + visible;
        }
        catch (SystemException e)
        {
            throw new PaymentDomainException($"Failed to mask the card number", e);
        }
    }

    private void SetExpiryDate(string expiryDate)
    {
        if (string.IsNullOrEmpty(expiryDate))
        {
            throw new PaymentDomainException($"The expiry date must be provided.");
        }

        try
        {
            var expiry = expiryDate.Split('/');
            ExpiryMonth = int.Parse(expiry[0]);
            ExpiryYear = int.Parse(expiry[1]);
        }
        catch (SystemException e)
        {
            throw new PaymentDomainException($"Invalid expiry date {expiryDate}", e);
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
        yield return MaskedNumber;
        yield return Holder;
        yield return ExpiryMonth;
        yield return ExpiryYear;
        yield return Cvv;
    }
}

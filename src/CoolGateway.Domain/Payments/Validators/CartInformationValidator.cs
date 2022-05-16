using FluentValidation;

namespace CoolGateway.Domain.Payments.Validators;

internal class CartInformationValidator : AbstractValidator<CardInformation>
{
    /// <summary>
    /// Refer to ISO IEC 7813.
    /// </summary>
    private const int MinCardHolderNameLength = 2;
    private const int MaxCardHolderNameLength = 26;

    public CartInformationValidator()
    {
        RuleFor(x => x.Number).CreditCard();
        RuleFor(x => x.Holder).MinimumLength(MinCardHolderNameLength).MaximumLength(MaxCardHolderNameLength);
        RuleFor(x => x.ExpiryMonth).GreaterThanOrEqualTo(1).LessThanOrEqualTo(12);
        RuleFor(x => x.ExpiryYear).LessThanOrEqualTo(99);
        RuleFor(x => x.Cvv).GreaterThanOrEqualTo(0).LessThanOrEqualTo(999);
    }
}

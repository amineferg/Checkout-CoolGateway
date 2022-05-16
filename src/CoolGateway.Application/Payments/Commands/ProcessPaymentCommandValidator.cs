using FluentValidation;

namespace CoolGateway.Application.Payments.Commands;

internal class ProcessPaymentCommandValidator : AbstractValidator<ProcessPaymentCommand>
{
    /// <summary>
    /// Refer to ISO IEC 7813.
    /// </summary>
    private const int MinCardHolderNameLength = 2;
    private const int MaxCardHolderNameLength = 26;

    public ProcessPaymentCommandValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.CardNumber).CreditCard();
        RuleFor(x => x.CardHolder).MinimumLength(MinCardHolderNameLength).MaximumLength(MaxCardHolderNameLength);
        RuleFor(x => x.CardExpiryMonth).GreaterThanOrEqualTo(1).LessThanOrEqualTo(12);
        RuleFor(x => x.CardExpiryYear).LessThanOrEqualTo(99);
        RuleFor(x => x.CardCvv).GreaterThanOrEqualTo(0).LessThanOrEqualTo(999);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty();
    }
}

using FluentValidation;

namespace CoolGateway.Domain.Payments.Validators;

internal class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.CardInformation).NotNull();
        RuleFor(x => x.Amount.Value).GreaterThan(0);
        RuleFor(x => x.Amount.Currency).NotEmpty();
    }
}

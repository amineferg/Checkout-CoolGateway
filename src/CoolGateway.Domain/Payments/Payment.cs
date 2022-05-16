using CoolGateway.Domain.Payments.Exceptions;
using CoolGateway.Domain.Payments.Validators;
using CoolGateway.SharedKernel.Models;

namespace CoolGateway.Domain.Payments;

public class Payment : Entity
{
    public Payment(Guid id, Guid merchantId, CardInformation cardInformation, PaymentAmount amount)
        : base(id)
    {
        MerchantId = merchantId;
        CardInformation = cardInformation;
        Amount = amount;

        Validate();
    }

    public Guid MerchantId { get; }

    public CardInformation CardInformation { get; }

    public PaymentAmount Amount { get; }

    private void Validate()
    {
        var validator = new PaymentValidator();
        var validationResult = validator.Validate(this);
        if (!validationResult.IsValid)
        {
            throw new PaymentDomainValidationException(
                $"Invalid domain model {nameof(Payment)} : " +
                $"{string.Join(", ", validationResult.Errors.Select(e => $"{e.PropertyName} => {e.ErrorMessage}").ToArray())}");
        }
    }
}

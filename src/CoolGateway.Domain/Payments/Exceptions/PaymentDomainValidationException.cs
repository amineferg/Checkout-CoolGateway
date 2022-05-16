namespace CoolGateway.Domain.Payments.Exceptions;

public class PaymentDomainValidationException : PaymentDomainException
{
    public PaymentDomainValidationException()
    {
    }

    public PaymentDomainValidationException(string message)
        : base(message)
    {
    }

    public PaymentDomainValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

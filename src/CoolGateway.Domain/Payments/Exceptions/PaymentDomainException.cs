using CoolGateway.Domain.Common;

namespace CoolGateway.Domain.Payments.Exceptions;

public class PaymentDomainException : CoolGatewayDomainException
{
    public PaymentDomainException()
    {
    }

    public PaymentDomainException(string message)
        : base(message)
    {
    }

    public PaymentDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

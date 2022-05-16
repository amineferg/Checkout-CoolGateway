using CoolGateway.SharedKernel.Exceptions;

namespace CoolGateway.Domain.Common;

public class CoolGatewayDomainException : CoolGatewayException
{
    public CoolGatewayDomainException()
    {
    }

    public CoolGatewayDomainException(string message)
        : base (message)
    {
    }

    public CoolGatewayDomainException(string message, Exception innerException)
        : base (message, innerException)
    {
    }
}

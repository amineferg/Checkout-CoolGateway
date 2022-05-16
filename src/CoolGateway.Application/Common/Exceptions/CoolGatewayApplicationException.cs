using CoolGateway.SharedKernel.Exceptions;

namespace CoolGateway.Application.Common.Exceptions;

public class CoolGatewayApplicationException : CoolGatewayException
{
    public CoolGatewayApplicationException()
    {
    }

    public CoolGatewayApplicationException(string message)
        : base(message)
    {
    }

    public CoolGatewayApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

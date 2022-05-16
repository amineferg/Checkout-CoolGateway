namespace CoolGateway.Application.Common.Exceptions;

public class ApplicationQueryException : CoolGatewayApplicationException
{
    public ApplicationQueryException()
    {
    }

    public ApplicationQueryException(string message)
        : base(message)
    {
    }

    public ApplicationQueryException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

namespace CoolGateway.Application.Common.Exceptions;

public class ApplicationValidationException : CoolGatewayApplicationException
{
    public ApplicationValidationException()
    {
    }

    public ApplicationValidationException(string message)
        : base(message)
    {
    }

    public ApplicationValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

namespace CoolGateway.Application.Common.Exceptions;

public class ApplicationPaymentRejectedException : CoolGatewayApplicationException
{
    public ApplicationPaymentRejectedException()
    {
    }

    public ApplicationPaymentRejectedException(string message)
        : base(message)
    {
    }

    public ApplicationPaymentRejectedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

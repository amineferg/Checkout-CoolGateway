namespace CoolGateway.SharedKernel.Exceptions;

public class CoolGatewayException : Exception
{
    public CoolGatewayException()
    {
    }

    public CoolGatewayException(string message)
        : base(message)
    {
    }

    public CoolGatewayException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

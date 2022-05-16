namespace CoolGateway.Infrastructure.Bank;

/// <summary>
/// Provides functionalities to resolve the financial institution issuing the card based on the Bank Identification Number (BIN)
/// also called the Issuer Identification Number (IIN).
/// </summary>
internal static class BinResolver
{
    public static IBank Resolve(string bin)
    {
        return bin switch
        {
            "123456" => new CoolBank(),
            _ => null
        };
    }
}

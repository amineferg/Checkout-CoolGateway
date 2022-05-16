using CoolGateway.Infrastructure.Persistence;

namespace CoolGateway.Api.IntegrationTests.Common;

public static class Utilities
{
    public static Guid SeedId = Guid.NewGuid();

    internal static void InitializeDbForTests(CoolGatewayDbContext db)
    {
        db.Payments.Add(new Infrastructure.Persistence.Models.PaymentDto
        {
            Id = SeedId,
            MerchantId = Guid.NewGuid(),
            CardNumber = "1111222233334444",
            CardHolder = "Seed First",
            CardExpiryMonth = 6,
            CardExpiryYear = 24,
            CardCvv = 222,
            Amount = 200,
            Currency = "COOL"
        });
        db.SaveChanges();
    }
}

using CoolGateway.Domain.Payments;
using CoolGateway.Domain.Payments.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoolGateway.Domain.UnitTests;

public class PaymentTests
{
    [Fact]
    public void ShouldBeValid()
    {
        var cardInformation = new CardInformation("1111222233334444", "test", 1, 99, 123);
        var paymentAmount = new PaymentAmount(10, "a");
        var exception = Record.Exception(() => new Payment(Guid.NewGuid(), Guid.NewGuid(), cardInformation, paymentAmount));

        exception.Should().BeNull();
    }

    [Fact]
    public void ShouldBeInvalid_NullCardInformation()
    {
        var paymentAmount = new PaymentAmount(10, "a");
        var exception = Record.Exception(() => new Payment(Guid.NewGuid(), Guid.NewGuid(), null, paymentAmount));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<PaymentDomainValidationException>();
    }

    [Fact]
    public void ShouldBeInvalid_InvalidPaymentAmount()
    {
        var cardInformation = new CardInformation("1111222233334444", "test", 1, 99, 123);
        var paymentAmount = new PaymentAmount(-10, "a");
        var exception = Record.Exception(() => new Payment(Guid.NewGuid(), Guid.NewGuid(), cardInformation, paymentAmount));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<PaymentDomainValidationException>();
    }
}

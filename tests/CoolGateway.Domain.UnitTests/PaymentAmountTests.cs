using CoolGateway.Domain.Payments;
using FluentAssertions;
using Xunit;

namespace CoolGateway.Domain.UnitTests;

public class PaymentAmountTests
{
    [Fact]
    public void ShouldBeEqualWithObjectWithSameProperties()
    {
        var paymentAmount1 = new PaymentAmount(10, "a");
        var paymentAmount2 = new PaymentAmount(10, "a");

        paymentAmount1.Equals(paymentAmount2).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotBeEqualWithObjectWithDifferentProperties()
    {
        var paymentAmount1 = new PaymentAmount(10, "b");
        var paymentAmount2 = new PaymentAmount(20, "a");

        paymentAmount1.Equals(paymentAmount2).Should().BeFalse();
    }
}

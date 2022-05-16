using CoolGateway.Domain.Payments;
using CoolGateway.Domain.Payments.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoolGateway.Domain.UnitTests;

public class CardInformationTests
{
    [Fact]
    public void ShouldBeEqualWithObjectWithSameProperties()
    {
        var cardInformation1 = new CardInformation("1111222233334444", "test", 1, 99, 123);
        var cardInformation2 = new CardInformation("1111222233334444", "test", 1, 99, 123);

        cardInformation1.Equals(cardInformation2).Should().BeTrue();
    }

    [Fact]
    public void ShouldNotBeEqualWithObjectWithDifferentProperties()
    {
        var cardInformation1 = new CardInformation("1111222233334444", "test", 1, 99, 123);
        var cardInformation2 = new CardInformation("1111222233334444", "test2", 2, 1, 456);

        cardInformation1.Equals(cardInformation2).Should().BeFalse();
    }


    [Fact]
    public void ShouldBeValid()
    {
        var exception = Record.Exception(() => new CardInformation("1111222233334444", "test", 1, 99, 123));

        exception.Should().BeNull();
    }

    [Fact]
    public void ShouldBeInvalid()
    {
        var exception = Record.Exception(() => new CardInformation("44445555666677778888", "test", 1, 99, 123));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<PaymentDomainValidationException>();
    }

    [Fact]
    public void ShouldSetTheMaskedNumber()
    {
        var cardInformation = new CardInformation("1111222233334444", "test", 1, 99, 123);

        cardInformation.MaskedNumber.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void MaskedNumberShouldHaveSameLengthAsCardNumber()
    {
        var cardInformation = new CardInformation("1111222233334444", "test", 1, 99, 123);

        cardInformation.MaskedNumber.Length.Equals(cardInformation.Number.Length).Should().BeTrue();
    }

    [Fact]
    public void ShouldHaveValidMaskedNumber()
    {
        var cardInformation = new CardInformation("1111222233334444", "test", 1, 99, 123);
        var expectedMaskedNumber = "************4444";

        cardInformation.MaskedNumber.Should().Be(expectedMaskedNumber);
    }

    [Fact]
    public void ShouldSetExpiryMonthAndYear()
    {
        var cardInformation = new CardInformation("1111222233334444", "test", "01/99", 123);
        var expectedExpiryMonth = 1;
        var expectedExpiryYear = 99;

        cardInformation.ExpiryMonth.Should().Be(expectedExpiryMonth);
        cardInformation.ExpiryYear.Should().Be(expectedExpiryYear);
    }
}

using CoolGateway.Application.Common.Exceptions;
using CoolGateway.Application.Payments.Queries;
using CoolGateway.Domain.Payments;
using CoolGateway.Domain.Payments.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoolGateway.Application.UnitTests.Payments;

public class GetPaymentQueryHandlerUnitTests
{
    [Fact]
    public async Task GetQueryPaymentHandlerShouldReturnPayment()
    {
        var paymentId = Guid.NewGuid();
        var cardInformation = new CardInformation("1111222233334444", "test", 1, 99, 123);
        var paymentAmount = new PaymentAmount(10, "a");
        var payment = new Payment(paymentId, Guid.NewGuid(), cardInformation, paymentAmount);
        var expectedPaymentQueryDto = new PaymentQueryDto
        {
            Id = paymentId,
            MerchantId = payment.MerchantId,
            CardNumber = cardInformation.MaskedNumber,
            CardHolder = cardInformation.Holder,
            CardExpiryMonth = cardInformation.ExpiryMonth,
            CardExpiryYear = cardInformation.ExpiryYear,
            CardCvv = cardInformation.Cvv,
            Amount = paymentAmount.Value,
            Currency = paymentAmount.Currency
        };

        var paymentQuery = new GetPaymentQuery { PaymentId = paymentId };

        var mockPaymentsRepository = new Mock<IPaymentsRepository>();
        mockPaymentsRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(payment);

        var sut = new GetPaymentQueryHandler(mockPaymentsRepository.Object);

        var result = await sut.Handle(paymentQuery, CancellationToken.None);

        result.Should().BeOfType<PaymentQueryDto>();
        result.Id.Should().Be(expectedPaymentQueryDto.Id);
        result.MerchantId.Should().Be(expectedPaymentQueryDto.MerchantId);
        result.CardNumber.Should().Be(expectedPaymentQueryDto.CardNumber);
        result.CardHolder.Should().Be(expectedPaymentQueryDto.CardHolder);
        result.CardExpiryMonth.Should().Be(expectedPaymentQueryDto.CardExpiryMonth);
        result.CardExpiryYear.Should().Be(expectedPaymentQueryDto.CardExpiryYear);
        result.CardCvv.Should().Be(expectedPaymentQueryDto.CardCvv);
        result.Amount.Should().Be(expectedPaymentQueryDto.Amount);
        result.Currency.Should().Be(expectedPaymentQueryDto.Currency);
    }

    [Fact]
    public async Task GetQueryPaymentHandlerShouldReturnApplicationQueryException()
    {
        Payment payment = null;
        var paymentQuery = new GetPaymentQuery { PaymentId = Guid.NewGuid() };

        var mockPaymentsRepository = new Mock<IPaymentsRepository>();
        mockPaymentsRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(payment);

        var sut = new GetPaymentQueryHandler(mockPaymentsRepository.Object);

        var exception = await Record.ExceptionAsync(() => sut.Handle(paymentQuery, CancellationToken.None));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<ApplicationQueryException>();
    }
}

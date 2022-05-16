using CoolGateway.Application.Common;
using CoolGateway.Application.Common.Exceptions;
using CoolGateway.Application.Common.Models;
using CoolGateway.Application.Payments.Commands;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoolGateway.Application.UnitTests.Payments;

public class ProcessPaymentCommandHandlerUnitTests
{
    [Fact]
    public async Task ProcessPaymentCommandHandlerShould_SavePayment()
    {
        var paymentCommand = new ProcessPaymentCommand
        {
            MerchantId = Guid.NewGuid(),
            CardNumber = "1111222233334444",
            CardHolder = "test",
            CardExpiryMonth = 1,
            CardExpiryYear = 99,
            CardCvv = 123,
            Amount = 10,
            Currency = "a"
        };

        var mockBankClient = new Mock<IBankClient>();
        mockBankClient.Setup(x => x.ProcessPayment(
            It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new BankResponse(Guid.NewGuid(), "Success"));

        var fakePaymentRepository = new FakePaymentsRepository();

        var sut = new ProcessPaymentCommandHandler(mockBankClient.Object, fakePaymentRepository);

        var result = await sut.Handle(paymentCommand, CancellationToken.None);

        var saved = await fakePaymentRepository.GetByIdAsync(result);

        result.Equals(Guid.Empty).Should().BeFalse();
        saved.Should().NotBeNull();
        saved.Id.Should().Be(result);
        saved.MerchantId.Should().Be(paymentCommand.MerchantId);
        saved.CardInformation.Should().NotBeNull();
        saved.CardInformation.Number.Should().Be(paymentCommand.CardNumber);
        saved.CardInformation.Holder.Should().Be(paymentCommand.CardHolder);
        saved.CardInformation.ExpiryMonth.Should().Be(paymentCommand.CardExpiryMonth);
        saved.CardInformation.ExpiryYear.Should().Be(paymentCommand.CardExpiryYear);
        saved.CardInformation.Cvv.Should().Be(paymentCommand.CardCvv);
        saved.Amount.Should().NotBeNull();
        saved.Amount.Value.Should().Be(paymentCommand.Amount);
        saved.Amount.Currency.Should().Be(paymentCommand.Currency);
    }

    [Fact]
    public async Task ProcessPaymentCommandHandlerShould_ThrowApplicationValidationException()
    {
        var paymentCommand = new ProcessPaymentCommand
        {
            MerchantId = Guid.NewGuid(),
            CardNumber = "invalid",
            CardHolder = "test",
            CardExpiryMonth = 1,
            CardExpiryYear = 99,
            CardCvv = 123,
            Amount = 10,
            Currency = "a"
        };

        var mockBankClient = new Mock<IBankClient>();

        var fakePaymentRepository = new FakePaymentsRepository();

        var sut = new ProcessPaymentCommandHandler(mockBankClient.Object, fakePaymentRepository);

        var exception = await Record.ExceptionAsync(() => sut.Handle(paymentCommand, CancellationToken.None));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<ApplicationValidationException>();
    }

    [Fact]
    public async Task ProcessPaymentCommandHandlerShould_ThrowApplicationPaymentRejectedException()
    {
        var paymentCommand = new ProcessPaymentCommand
        {
            MerchantId = Guid.NewGuid(),
            CardNumber = "1111222233334444",
            CardHolder = "test",
            CardExpiryMonth = 1,
            CardExpiryYear = 99,
            CardCvv = 123,
            Amount = 10,
            Currency = "a"
        };

        var mockBankClient = new Mock<IBankClient>();
        mockBankClient.Setup(x => x.ProcessPayment(
            It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new BankResponse("Invalid"));

        var fakePaymentRepository = new FakePaymentsRepository();

        var sut = new ProcessPaymentCommandHandler(mockBankClient.Object, fakePaymentRepository);

        var exception = await Record.ExceptionAsync(() => sut.Handle(paymentCommand, CancellationToken.None));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<ApplicationPaymentRejectedException>();
    }
}

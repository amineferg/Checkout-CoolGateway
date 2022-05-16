using CoolGateway.Application.Common;
using CoolGateway.Application.Common.Exceptions;
using CoolGateway.Domain.Payments;
using CoolGateway.Domain.Payments.Repositories;
using MediatR;

namespace CoolGateway.Application.Payments.Commands;

internal class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Guid>
{
    private readonly IBankClient _bankClient;
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly ProcessPaymentCommandValidator _validator;

    public ProcessPaymentCommandHandler(IBankClient bankClient, IPaymentsRepository paymentsRepository)
    {
        _bankClient = bankClient;
        _paymentsRepository = paymentsRepository;
        _validator = new ProcessPaymentCommandValidator();
    }

    public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            throw new ApplicationValidationException($"Invalid command {nameof(ProcessPaymentCommand)} : " +
                $"{string.Join(", ", validationResult.Errors.Select(e => $"{e.PropertyName} => {e.ErrorMessage}").ToArray())}");
        }

        var bankResponse = _bankClient.ProcessPayment(
            request.MerchantId,
            request.CardNumber,
            request.CardHolder,
            request.CardExpiryMonth,
            request.CardExpiryYear,
            request.CardCvv,
            request.Amount,
            request.Currency);

        if (bankResponse.Status != "Success")
        {
            throw new ApplicationPaymentRejectedException($"The payment was rejected with status : {bankResponse.Status}.");
        }

        var cardInformation = new CardInformation(
            request.CardNumber,
            request.CardHolder,
            request.CardExpiryMonth,
            request.CardExpiryYear,
            request.CardCvv);

        var payment = new Payment(Guid.NewGuid(), request.MerchantId, cardInformation, new PaymentAmount(request.Amount, request.Currency));
        _paymentsRepository.Create(payment);
        await _paymentsRepository.SaveAsync();

        return payment.Id;
    }
}

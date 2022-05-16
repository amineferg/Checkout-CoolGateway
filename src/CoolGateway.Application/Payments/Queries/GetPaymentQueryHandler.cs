using CoolGateway.Application.Common;
using CoolGateway.Application.Common.Exceptions;
using CoolGateway.Domain.Payments.Repositories;
using MediatR;

namespace CoolGateway.Application.Payments.Queries;

internal class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentQueryDto>
{
    private readonly IPaymentsRepository _paymentsRepository;

    public GetPaymentQueryHandler(IPaymentsRepository paymentRepository)
    {
        _paymentsRepository = paymentRepository;
    }

    public async Task<PaymentQueryDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentsRepository.GetByIdAsync(request.PaymentId);

        if (payment == null)
        {
            throw new ApplicationQueryException($"No payment with ID {request.PaymentId} was found.");
        }

        return new PaymentQueryDto
        {
            Id = payment.Id,
            MerchantId = payment.MerchantId,
            CardNumber = payment.CardInformation.MaskedNumber,
            CardHolder = payment.CardInformation.Holder,
            CardExpiryMonth = payment.CardInformation.ExpiryMonth,
            CardExpiryYear = payment.CardInformation.ExpiryYear,
            CardCvv = payment.CardInformation.Cvv,
            Amount = payment.Amount.Value,
            Currency = payment.Amount.Currency
        };
    }
}

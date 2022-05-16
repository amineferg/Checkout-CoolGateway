using MediatR;

namespace CoolGateway.Application.Payments.Queries;

public class GetPaymentQuery : IRequest<PaymentQueryDto>
{
    public Guid PaymentId { get; set; }
}

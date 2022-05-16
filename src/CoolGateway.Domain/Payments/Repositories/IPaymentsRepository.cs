using CoolGateway.SharedKernel.Repositories;

namespace CoolGateway.Domain.Payments.Repositories;

public interface IPaymentsRepository : IReadWriteRepository<Payment, Guid>
{
}

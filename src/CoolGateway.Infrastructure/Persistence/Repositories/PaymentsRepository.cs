using CoolGateway.Domain.Payments;
using CoolGateway.Domain.Payments.Repositories;
using CoolGateway.Infrastructure.Persistence.Common;
using CoolGateway.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CoolGateway.Infrastructure.Persistence.Repositories;

internal class PaymentsRepository : AReadWriteRepository<Payment, Guid>, IPaymentsRepository
{
    public PaymentsRepository(CoolGatewayDbContext dbContext)
        : base(dbContext)
    {
    }

    public override void Create(Payment entity)
    {
        var paymentDto = new PaymentDto();
        paymentDto.MapFrom(entity);
        DbContext.Payments.Add(paymentDto);
    }

    public override async Task<Payment> GetByIdAsync(Guid id)
    {
        if (id == default)
        {
            return null;
        }

        return await DbContext.Payments
            .Where(p => p.Id == id)
            .Select(p => p.MapTo())
            .SingleOrDefaultAsync();
    }
}

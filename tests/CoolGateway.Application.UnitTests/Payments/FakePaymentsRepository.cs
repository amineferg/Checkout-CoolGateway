using CoolGateway.Application.UnitTests.Common;
using CoolGateway.Domain.Payments;
using CoolGateway.Domain.Payments.Repositories;

namespace CoolGateway.Application.UnitTests.Payments;

public class FakePaymentsRepository : IPaymentsRepository
{
    public readonly FakeEntityStore<Payment> Store = new FakeEntityStore<Payment>();
    private int _counter = 0;

    public void Create(Payment entity)
    {
        Store.Add(entity);
        _counter++;
    }

    public Task<Payment> GetByIdAsync(Guid id)
    {
        return Task.FromResult(Store.GetById(id));
    }

    public async Task<int> SaveAsync()
    {
        var result = await Task.FromResult(_counter);
        _counter = 0;
        return result;
    }
}

using CoolGateway.SharedKernel.Models;

namespace CoolGateway.Application.UnitTests.Common;

public class FakeEntityStore<TEntity> : List<TEntity> where TEntity : Entity
{
    public new void Add(TEntity entity)
    {
        base.Add(entity);
    }

    public TEntity GetById(Guid id) => this.FirstOrDefault(e => e.Id == id);
}

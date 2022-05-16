using CoolGateway.SharedKernel.Models;
using CoolGateway.SharedKernel.Repositories;

namespace CoolGateway.Infrastructure.Persistence.Common;

internal abstract class AReadWriteRepository<TEntity, TId> : AReadOnlyRepository<TEntity, TId>, IReadWriteRepository<TEntity, TId> where TEntity : Entity<TId>
{
    public AReadWriteRepository(CoolGatewayDbContext dbContext)
        : base(dbContext)
    {
    }

    public abstract void Create(TEntity entity);

    public async Task<int> SaveAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}

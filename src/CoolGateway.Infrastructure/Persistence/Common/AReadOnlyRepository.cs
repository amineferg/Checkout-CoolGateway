using CoolGateway.SharedKernel.Models;
using CoolGateway.SharedKernel.Repositories;

namespace CoolGateway.Infrastructure.Persistence.Common;

internal abstract class AReadOnlyRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId> where TEntity : Entity<TId>
{
    protected readonly CoolGatewayDbContext DbContext;

    public AReadOnlyRepository(CoolGatewayDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public abstract Task<TEntity> GetByIdAsync(TId id);
}

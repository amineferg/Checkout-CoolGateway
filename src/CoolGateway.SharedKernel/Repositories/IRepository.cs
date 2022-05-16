using CoolGateway.SharedKernel.Models;

namespace CoolGateway.SharedKernel.Repositories;

public interface IRepository
{
}

public interface IReadOnlyRepository<TEntity, TId> : IRepository where TEntity : Entity<TId>
{
    Task<TEntity> GetByIdAsync(TId id);
}

public interface IReadWriteRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId> where TEntity : Entity<TId>
{
    void Create(TEntity entity);

    Task<int> SaveAsync();
}
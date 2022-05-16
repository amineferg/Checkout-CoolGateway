using CoolGateway.SharedKernel.Models;

namespace CoolGateway.Infrastructure.Persistence.Models;

internal abstract class EntityDto
{
    public Guid Id { get; set; }
}

internal abstract class EntityDto<TEntity> : EntityDto where TEntity : Entity
{
    public abstract TEntity MapTo();

    public abstract void MapFrom(TEntity source);
}

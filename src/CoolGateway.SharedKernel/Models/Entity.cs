namespace CoolGateway.SharedKernel.Models;

public abstract class Entity<TId>
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; private set; }

    protected object Actual => this;

    public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;

    public DateTime? ModifiedDate { get; protected set; }

    public override bool Equals(object obj)
    {
        if (!(obj is Entity<TId> other))
        {
            return false;
        }

        if (ReferenceEquals(other, this))
        {
            return true;
        }

        if (Actual.GetType() != other.Actual.GetType())
        {
            return false;
        }

        if (IsNullOrDefault() || other.IsNullOrDefault())
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    private bool IsNullOrDefault()
    {
        return Id is null || Id.Equals(default(TId));
    }

    public override int GetHashCode()
    {
        return (Actual.GetType().ToString() + Id).GetHashCode();
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !(a == b);
    }
}

public abstract class Entity : Entity<Guid>
{
    protected Entity(Guid id)
        : base(id)
    {
    }
}

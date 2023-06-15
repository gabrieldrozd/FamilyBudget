using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyBudget.Common.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid ExternalId { get; private init; }

    protected Entity(Guid externalId)
    {
        ExternalId = externalId;
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    public bool Equals(Entity other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return ExternalId == other.ExternalId;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return ExternalId == entity.ExternalId;
    }

    public override int GetHashCode()
    {
        return ExternalId.GetHashCode();
    }
}
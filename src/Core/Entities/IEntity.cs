namespace Core.Entities;

/// <summary>
/// Minimal entity contract used by the generic repository examples.
/// A shared key contract lets generic code locate entities without knowing their concrete type.
/// </summary>
public interface IEntity
{
    int Id { get; set; }
}

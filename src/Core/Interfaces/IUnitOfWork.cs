namespace Core.Interfaces;

/// <summary>
/// Coordinates one atomic commit across repositories sharing the same persistence context.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

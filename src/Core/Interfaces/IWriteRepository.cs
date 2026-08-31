using Core.Entities;

namespace Core.Interfaces;

/// <summary>
/// Command-side repository contract. Methods stage changes; IUnitOfWork commits the transaction.
/// Splitting reads and writes is useful when CQRS-style responsibilities need to remain explicit.
/// </summary>
public interface IWriteRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

/// <summary>
/// Read-only repository contract. Use this narrow interface for queries and read-only services so
/// callers cannot accidentally modify state (Interface Segregation Principle).
/// </summary>
public interface IReadRepository<TEntity> where TEntity : class, IEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> ListAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);
}

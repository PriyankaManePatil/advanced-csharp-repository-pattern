using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories;

/// <summary>
/// Read-repository decorator that adds caching without changing the underlying repository.
/// Specification results are intentionally not cached because a safe key requires domain knowledge.
/// </summary>
public sealed class CachedReadRepository<TEntity>(IReadRepository<TEntity> inner, IMemoryCache cache)
    : IReadRepository<TEntity> where TEntity : class, IEntity
{
    private static string AllKey => $"{typeof(TEntity).FullName}:all";
    private static string IdKey(int id) => $"{typeof(TEntity).FullName}:{id}";

    /// <inheritdoc />
    /// <remarks>Uses one entity-type key because the complete collection has no input parameters.</remarks>
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await cache.GetOrCreateAsync(AllKey, _ => inner.GetAllAsync(cancellationToken)) ?? [];

    /// <inheritdoc />
    /// <remarks>The entity ID is part of the key so cached records cannot collide.</remarks>
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await cache.GetOrCreateAsync(IdKey(id), _ => inner.GetByIdAsync(id, cancellationToken));

    /// <inheritdoc />
    /// <remarks>Delegates directly because expressions do not provide a reliable general-purpose cache key.</remarks>
    public Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
        inner.ListAsync(specification, cancellationToken);
}

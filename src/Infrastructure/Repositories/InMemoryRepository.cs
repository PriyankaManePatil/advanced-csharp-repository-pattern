using System.Collections.Concurrent;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Repositories;

/// <summary>
/// Lightweight repository test double. It demonstrates an alternative implementation of the same
/// abstraction; it is not a substitute for integration tests against the real database provider.
/// </summary>
public sealed class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ConcurrentDictionary<int, TEntity> entities = new();

    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<TEntity>>(entities.Values.ToList());

    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        Task.FromResult(entities.GetValueOrDefault(id));

    public Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        // Compile is acceptable in a test double; production EF repositories translate the expression to SQL.
        IEnumerable<TEntity> query = entities.Values;
        if (specification.Criteria is not null) query = query.Where(specification.Criteria.Compile());
        if (specification.OrderBy is not null) query = query.OrderBy(specification.OrderBy.Compile());
        else if (specification.OrderByDescending is not null) query = query.OrderByDescending(specification.OrderByDescending.Compile());
        return Task.FromResult<IReadOnlyList<TEntity>>(query.ToList());
    }

    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (!entities.TryAdd(entity.Id, entity)) throw new InvalidOperationException($"Entity {entity.Id} already exists.");
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (!entities.ContainsKey(entity.Id)) return Task.FromResult(false);
        entities[entity.Id] = entity;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        Task.FromResult(entities.TryRemove(id, out _));
}

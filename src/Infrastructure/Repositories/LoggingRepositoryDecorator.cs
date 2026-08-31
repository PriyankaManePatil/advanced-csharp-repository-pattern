using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

/// <summary>Decorator example that adds diagnostics while preserving the repository contract.</summary>
public sealed class LoggingRepositoryDecorator<TEntity>(
    IRepository<TEntity> inner,
    ILogger<LoggingRepositoryDecorator<TEntity>> logger) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        inner.GetAllAsync(cancellationToken);

    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        inner.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
        inner.ListAsync(specification, cancellationToken);

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Staging creation of {EntityType}", typeof(TEntity).Name);
        return await inner.AddAsync(entity, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Staging update of {EntityType} {EntityId}", typeof(TEntity).Name, entity.Id);
        return await inner.UpdateAsync(entity, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Staging deletion of {EntityType} {EntityId}", typeof(TEntity).Name, id);
        return await inner.DeleteAsync(id, cancellationToken);
    }
}

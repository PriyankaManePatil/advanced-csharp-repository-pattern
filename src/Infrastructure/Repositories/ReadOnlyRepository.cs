using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>Generic query-only repository for workloads that must not modify data.</summary>
public class ReadOnlyRepository<TEntity>(AppDbContext context) : IReadRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>Provides protected query access for specialised read repositories.</summary>
    protected DbSet<TEntity> Entities { get; } = context.Set<TEntity>();

    /// <inheritdoc />
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Entities.AsNoTracking().ToListAsync(cancellationToken);

    /// <inheritdoc />
    public virtual Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        Entities.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    /// <inheritdoc />
    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
        await SpecificationEvaluator.Apply(Entities, specification).ToListAsync(cancellationToken);
}

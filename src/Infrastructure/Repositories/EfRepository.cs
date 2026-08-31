using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Generic EF Core repository. Write methods stage changes; IUnitOfWork commits them atomically.
/// </summary>
public class EfRepository<TEntity>(AppDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected DbSet<TEntity> Entities { get; } = context.Set<TEntity>();

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Entities.AsNoTracking().ToListAsync(cancellationToken);

    public virtual Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        Entities.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
        await SpecificationEvaluator.Apply(Entities, specification).ToListAsync(cancellationToken);

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await Entities.AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (!await Entities.AnyAsync(candidate => candidate.Id == entity.Id, cancellationToken)) return false;
        Entities.Update(entity);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FindAsync([id], cancellationToken);
        if (entity is null) return false;
        Entities.Remove(entity);
        return true;
    }
}

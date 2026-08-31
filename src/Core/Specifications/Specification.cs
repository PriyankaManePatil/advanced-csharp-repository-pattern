using System.Linq.Expressions;

namespace Core.Specifications;

/// <summary>Base class that makes domain-specific specifications concise and readable.</summary>
public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; protected init; }
    public Expression<Func<TEntity, object>>? OrderBy { get; protected init; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; protected init; }
}

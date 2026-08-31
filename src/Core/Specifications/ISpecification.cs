using System.Linq.Expressions;

namespace Core.Specifications;

/// <summary>
/// Encapsulates reusable query intent without leaking IQueryable outside the data-access boundary.
/// </summary>
public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDescending { get; }
}

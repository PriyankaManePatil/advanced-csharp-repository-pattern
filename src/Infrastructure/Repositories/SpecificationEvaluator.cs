using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>Translates a framework-independent specification into an EF Core query.</summary>
internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(specification);
        if (specification.Criteria is not null) query = query.Where(specification.Criteria);
        if (specification.OrderBy is not null) query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending is not null) query = query.OrderByDescending(specification.OrderByDescending);
        return query.AsNoTracking();
    }
}

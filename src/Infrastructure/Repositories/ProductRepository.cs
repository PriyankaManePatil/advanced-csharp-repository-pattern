using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Repositories;

/// <summary>
/// Aggregate-specific repository: generic mechanics are reused, while product-specific queries retain
/// names from the business domain instead of exposing EF Core or IQueryable to the application.
/// </summary>
public sealed class ProductRepository(AppDbContext context) : EfRepository<Product>(context), IProductRepository
{
    public Task<IReadOnlyList<Product>> GetByPriceRangeAsync(
        decimal minimumPrice,
        decimal maximumPrice,
        CancellationToken cancellationToken = default) =>
        ListAsync(new ProductsInPriceRangeSpecification(minimumPrice, maximumPrice), cancellationToken);
}

using Core.Entities;

namespace Core.Interfaces;

/// <summary>
/// Aggregate-specific repository. It inherits standard operations and adds only product language.
/// This is normally clearer than exposing a generic repository directly to application services.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetByPriceRangeAsync(
        decimal minimumPrice,
        decimal maximumPrice,
        CancellationToken cancellationToken = default);
}

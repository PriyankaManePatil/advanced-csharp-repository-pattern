using Application.DTOs;

namespace Application.Interfaces;

/// <summary>
/// Defines product use cases available to delivery mechanisms such as HTTP, messaging or a console application.
/// The interface deliberately uses DTOs and never exposes DbContext, EF entities or IQueryable.
/// </summary>
public interface IProductService
{
    /// <summary>Returns every product as API-safe DTOs.</summary>
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Returns one product, or null when the identifier does not exist.</summary>
    Task<ProductDto?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);

    /// <summary>Validates, stages and commits a new product.</summary>
    Task<ProductDto> CreateAsync(SaveProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>Returns false when the target is missing; commits once when updated.</summary>
    Task<bool> UpdateAsync(int productId, SaveProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>Returns false when the target is missing; commits once when deleted.</summary>
    Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default);
}

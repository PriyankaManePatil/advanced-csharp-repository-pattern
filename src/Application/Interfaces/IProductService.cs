using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateAsync(SaveProductRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int productId, SaveProductRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default);
}

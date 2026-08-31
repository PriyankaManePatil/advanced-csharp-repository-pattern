using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default);
}

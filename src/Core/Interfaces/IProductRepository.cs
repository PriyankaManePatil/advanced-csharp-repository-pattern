using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// Defines contract for product data access.
    /// </summary>
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int productId);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int productId);
    }
}


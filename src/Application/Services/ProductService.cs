using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public sealed class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await repository.GetAllAsync(cancellationToken)).Select(Map).ToList();

    public async Task<ProductDto?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        EnsureValidId(productId);
        var product = await repository.GetByIdAsync(productId, cancellationToken);
        return product is null ? null : Map(product);
    }

    public async Task<ProductDto> CreateAsync(SaveProductRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        var product = new Product { Name = NormalizeName(request.Name), Price = EnsureValidPrice(request.Price) };
        return Map(await repository.AddAsync(product, cancellationToken));
    }

    public async Task<bool> UpdateAsync(int productId, SaveProductRequest request, CancellationToken cancellationToken = default)
    {
        EnsureValidId(productId);
        ArgumentNullException.ThrowIfNull(request);
        return await repository.UpdateAsync(new Product
        {
            ProductId = productId,
            Name = NormalizeName(request.Name),
            Price = EnsureValidPrice(request.Price)
        }, cancellationToken);
    }

    public Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default)
    {
        EnsureValidId(productId);
        return repository.DeleteAsync(productId, cancellationToken);
    }

    private static ProductDto Map(Product product) => new(product.ProductId, product.Name, product.Price);

    private static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name is required.", nameof(name));
        var normalized = name.Trim();
        if (normalized.Length > 200) throw new ArgumentException("Product name cannot exceed 200 characters.", nameof(name));
        return normalized;
    }

    private static decimal EnsureValidPrice(decimal price) =>
        price < 0 ? throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.") : price;

    private static void EnsureValidId(int productId)
    {
        if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be positive.");
    }
}

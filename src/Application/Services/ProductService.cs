using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

/// <summary>Coordinates validation, repository operations and the transaction boundary.</summary>
public sealed class ProductService(IProductRepository repository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        // Mapping here prevents EF entities from leaking through the application boundary.
        (await repository.GetAllAsync(cancellationToken)).Select(Map).ToList();

    public async Task<ProductDto?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        EnsureValidId(productId);
        var product = await repository.GetByIdAsync(productId, cancellationToken);
        return product is null ? null : Map(product);
    }

    public async Task<ProductDto> CreateAsync(SaveProductRequest request, CancellationToken cancellationToken = default)
    {
        // Guard clauses fail before any persistence work, keeping invalid state out of the repository.
        ArgumentNullException.ThrowIfNull(request);
        var product = new Product { Name = NormalizeName(request.Name), Price = EnsureValidPrice(request.Price) };
        var created = await repository.AddAsync(product, cancellationToken);
        // The repository stages the change; the service owns the business-operation commit boundary.
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Map(created);
    }

    public async Task<bool> UpdateAsync(int productId, SaveProductRequest request, CancellationToken cancellationToken = default)
    {
        EnsureValidId(productId);
        ArgumentNullException.ThrowIfNull(request);
        var updated = await repository.UpdateAsync(new Product
        {
            ProductId = productId,
            Name = NormalizeName(request.Name),
            Price = EnsureValidPrice(request.Price)
        }, cancellationToken);
        // A missing entity is an expected outcome, so it returns false and does not perform an empty commit.
        if (updated) await unitOfWork.SaveChangesAsync(cancellationToken);
        return updated;
    }

    public async Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default)
    {
        EnsureValidId(productId);
        var deleted = await repository.DeleteAsync(productId, cancellationToken);
        if (deleted) await unitOfWork.SaveChangesAsync(cancellationToken);
        return deleted;
    }

    private static ProductDto Map(Product product) => new(product.ProductId, product.Name, product.Price);

    private static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name is required.", nameof(name));
        // Normalising at one boundary makes stored and returned names consistent for every caller.
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

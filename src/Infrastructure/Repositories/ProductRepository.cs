using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Products.AsNoTracking().OrderBy(x => x.ProductId).ToListAsync(cancellationToken);

    public Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default) =>
        context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(product);
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (!await context.Products.AnyAsync(x => x.ProductId == product.ProductId, cancellationToken)) return false;
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FindAsync([productId], cancellationToken);
        if (product is null) return false;
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

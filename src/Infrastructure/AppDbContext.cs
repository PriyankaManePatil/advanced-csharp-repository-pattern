using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

/// <summary>
/// EF Core session for the sample application. A context tracks entity changes and forms the
/// concrete transaction boundary used by <see cref="EfUnitOfWork"/>.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>Provides query and change-tracking access to products.</summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Keeps database rules beside the persistence model: the key identifies a row, the required
    /// name has a safe maximum length, and decimal precision prevents provider-dependent rounding.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var product = modelBuilder.Entity<Product>();
        product.HasKey(x => x.Id);
        product.Property(x => x.Name).HasMaxLength(200).IsRequired();
        product.Property(x => x.Price).HasPrecision(18, 2);
    }
}

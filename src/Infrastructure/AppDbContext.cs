using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    /// <summary>
    /// Represents the Entity Framework Core database context.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}

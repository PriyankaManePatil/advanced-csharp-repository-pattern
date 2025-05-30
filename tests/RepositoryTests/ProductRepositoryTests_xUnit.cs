using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace RepositoryTests
{
    /// <summary>
    /// xUnit test class for ProductRepository. Preferred for integration with GitHub Actions.
    /// </summary>
    public class ProductRepositoryTests_xUnit
    {
        private AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddProduct()
        {
            var context = GetInMemoryDb();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Item A", Price = 150 };

            await repo.AddAsync(product);

            var all = await repo.GetAllAsync();
            Assert.Single(all);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct()
        {
            var context = GetInMemoryDb();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Item B", Price = 200 };
            await repo.AddAsync(product);

            var result = await repo.GetByIdAsync(product.ProductId);
            Assert.NotNull(result);
            Assert.Equal("Item B", result!.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            var context = GetInMemoryDb();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Item C", Price = 250 };
            await repo.AddAsync(product);

            await repo.DeleteAsync(product.ProductId);
            var result = await repo.GetByIdAsync(product.ProductId);

            Assert.Null(result);
        }
    }
}

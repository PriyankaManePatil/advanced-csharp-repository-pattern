using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace RepositoryTests
{
    /// <summary>
    /// NUnit test class for ProductRepository. Preferred when using NUnit test runners.
    /// </summary>
    public class ProductRepositoryTests_NUnit
    {
        private AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Test]
        public async Task AddAsync_ShouldAddProduct()
        {
            var context = GetInMemoryDb();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Item A", Price = 150 };

            await repo.AddAsync(product);
            var all = await repo.GetAllAsync();

            Assert.That(all, Has.Exactly(1).Items);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnProduct()
        {
            var context = GetInMemoryDb();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Item B", Price = 200 };
            await repo.AddAsync(product);

            var result = await repo.GetByIdAsync(product.ProductId);
            Assert.IsNotNull(result);
            Assert.That(result!.Name, Is.EqualTo("Item B"));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            var context = GetInMemoryDb();
            var repo = new ProductRepository(context);
            var product = new Product { Name = "Item C", Price = 250 };
            await repo.AddAsync(product);

            await repo.DeleteAsync(product.ProductId);
            var result = await repo.GetByIdAsync(product.ProductId);

            Assert.IsNull(result);
        }
    }
}

// =============================
// 💬 Recommendation:
// Use xUnit if you plan to run tests in GitHub Actions or .NET CLI environments
// Use NUnit if you're in enterprise test-heavy projects or using older CI/CD pipelines

using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests;

public sealed class ProductRepositoryTests : IAsyncDisposable
{
    private readonly AppDbContext context;
    private readonly ProductRepository repository;

    public ProductRepositoryTests()
    {
        context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        repository = new ProductRepository(context);
    }

    [Fact]
    public async Task CrudLifecycle_Works()
    {
        var created = await repository.AddAsync(new Product { Name = "Monitor", Price = 200 });
        Assert.True(created.ProductId > 0);
        Assert.Single(await repository.GetAllAsync());
        created.Name = "4K Monitor";
        Assert.True(await repository.UpdateAsync(created));
        Assert.Equal("4K Monitor", (await repository.GetByIdAsync(created.ProductId))!.Name);
        Assert.True(await repository.DeleteAsync(created.ProductId));
        Assert.Null(await repository.GetByIdAsync(created.ProductId));
    }

    [Fact]
    public async Task UpdateAndDelete_WhenMissing_ReturnFalse()
    {
        Assert.False(await repository.UpdateAsync(new Product { ProductId = 99, Name = "Missing", Price = 1 }));
        Assert.False(await repository.DeleteAsync(99));
    }

    public ValueTask DisposeAsync() => context.DisposeAsync();
}

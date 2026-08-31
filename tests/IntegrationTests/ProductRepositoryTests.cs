using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests;

public sealed class ProductRepositoryTests : IAsyncDisposable
{
    // A unique database name prevents state leaking between parallel test instances.
    private readonly AppDbContext context;
    private readonly ProductRepository repository;
    private readonly EfUnitOfWork unitOfWork;

    public ProductRepositoryTests()
    {
        context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        repository = new ProductRepository(context);
        unitOfWork = new EfUnitOfWork(context);
    }

    [Fact]
    public async Task CrudLifecycle_Works()
    {
        // Unlike unit tests, this exercises the real repository, DbContext and Unit of Work together.
        var created = await repository.AddAsync(new Product { Name = "Monitor", Price = 200 });
        await unitOfWork.SaveChangesAsync();
        Assert.True(created.ProductId > 0);
        Assert.Single(await repository.GetAllAsync());
        created.Name = "4K Monitor";
        Assert.True(await repository.UpdateAsync(created));
        await unitOfWork.SaveChangesAsync();
        Assert.Equal("4K Monitor", (await repository.GetByIdAsync(created.ProductId))!.Name);
        Assert.True(await repository.DeleteAsync(created.ProductId));
        await unitOfWork.SaveChangesAsync();
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

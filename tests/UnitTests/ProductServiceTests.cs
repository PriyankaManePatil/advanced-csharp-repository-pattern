using Application.DTOs;
using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Moq;
using Xunit;

namespace UnitTests;

public sealed class ProductServiceTests
{
    private readonly Mock<IProductRepository> repository = new();

    [Fact]
    public async Task CreateAsync_TrimsNameAndReturnsCreatedProduct()
    {
        repository.Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product product, CancellationToken _) => { product.ProductId = 1; return product; });
        var result = await new ProductService(repository.Object)
            .CreateAsync(new SaveProductRequest { Name = "  Keyboard  ", Price = 99.50m });
        Assert.Equal(new ProductDto(1, "Keyboard", 99.50m), result);
    }

    [Fact]
    public async Task CreateAsync_WithBlankName_ThrowsArgumentException() =>
        await Assert.ThrowsAsync<ArgumentException>(() => new ProductService(repository.Object)
            .CreateAsync(new SaveProductRequest { Name = " " }));

    [Fact]
    public async Task CreateAsync_WithNegativePrice_ThrowsArgumentOutOfRangeException() =>
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => new ProductService(repository.Object)
            .CreateAsync(new SaveProductRequest { Name = "Keyboard", Price = -1 }));

    [Fact]
    public async Task GetByIdAsync_WhenMissing_ReturnsNull()
    {
        repository.Setup(x => x.GetByIdAsync(42, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);
        Assert.Null(await new ProductService(repository.Object).GetByIdAsync(42));
    }

    [Fact]
    public async Task UpdateAsync_ForwardsNormalizedProduct()
    {
        repository.Setup(x => x.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        Assert.True(await new ProductService(repository.Object)
            .UpdateAsync(7, new SaveProductRequest { Name = " Mouse ", Price = 20 }));
        repository.Verify(x => x.UpdateAsync(It.Is<Product>(p => p.ProductId == 7 && p.Name == "Mouse"), It.IsAny<CancellationToken>()));
    }
}

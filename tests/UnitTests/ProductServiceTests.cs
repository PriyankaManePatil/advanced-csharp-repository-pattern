using Application.DTOs;
using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Moq;
using Xunit;

namespace UnitTests;

public sealed class ProductServiceTests
{
    // Unit tests isolate application decisions; EF Core is intentionally absent from this test project.
    private readonly Mock<IProductRepository> repository = new();
    private readonly Mock<IUnitOfWork> unitOfWork = new();

    [Fact]
    public async Task CreateAsync_TrimsNameAndReturnsCreatedProduct()
    {
        // Arrange: the repository simulates database-generated identity assignment.
        repository.Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product product, CancellationToken _) => { product.ProductId = 1; return product; });
        // Act: invoke the use case through the real application service.
        var result = await new ProductService(repository.Object, unitOfWork.Object)
            .CreateAsync(new SaveProductRequest { Name = "  Keyboard  ", Price = 99.50m });
        // Assert: verify observable output; other tests verify collaboration/commit behaviour.
        Assert.Equal(new ProductDto(1, "Keyboard", 99.50m), result);
    }

    [Fact]
    public async Task CreateAsync_WithBlankName_ThrowsArgumentException() =>
        await Assert.ThrowsAsync<ArgumentException>(() => new ProductService(repository.Object, unitOfWork.Object)
            .CreateAsync(new SaveProductRequest { Name = " " }));

    [Fact]
    public async Task CreateAsync_WithNegativePrice_ThrowsArgumentOutOfRangeException() =>
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => new ProductService(repository.Object, unitOfWork.Object)
            .CreateAsync(new SaveProductRequest { Name = "Keyboard", Price = -1 }));

    [Fact]
    public async Task GetByIdAsync_WhenMissing_ReturnsNull()
    {
        repository.Setup(x => x.GetByIdAsync(42, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);
        Assert.Null(await new ProductService(repository.Object, unitOfWork.Object).GetByIdAsync(42));
    }

    [Fact]
    public async Task UpdateAsync_ForwardsNormalizedProduct()
    {
        repository.Setup(x => x.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        Assert.True(await new ProductService(repository.Object, unitOfWork.Object)
            .UpdateAsync(7, new SaveProductRequest { Name = " Mouse ", Price = 20 }));
        repository.Verify(x => x.UpdateAsync(It.Is<Product>(p => p.ProductId == 7 && p.Name == "Mouse"), It.IsAny<CancellationToken>()));
        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

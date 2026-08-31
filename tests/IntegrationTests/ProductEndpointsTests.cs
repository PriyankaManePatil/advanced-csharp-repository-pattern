using System.Net;
using System.Net.Http.Json;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public sealed class ProductEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    // WebApplicationFactory starts the real ASP.NET Core pipeline in memory without opening a network port.
    private readonly HttpClient client;
    public ProductEndpointsTests(WebApplicationFactory<Program> factory) => client = factory.CreateClient();

    [Fact]
    public async Task CreateGetUpdateDelete_ReturnExpectedStatusCodes()
    {
        // One lifecycle test proves routing, JSON, DI, service, repository and status mapping work together.
        var create = await client.PostAsJsonAsync("/api/products", new SaveProductRequest { Name = "Laptop", Price = 800 });
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var product = await create.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(product);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync($"/api/products/{product.ProductId}")).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent,
            (await client.PutAsJsonAsync($"/api/products/{product.ProductId}", new SaveProductRequest { Name = "Laptop Pro", Price = 900 })).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await client.DeleteAsync($"/api/products/{product.ProductId}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/products/{product.ProductId}")).StatusCode);
    }
}

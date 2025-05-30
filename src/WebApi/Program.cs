using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure in-memory database (for demonstration)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Register repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Minimal API route for reference\app.MapGet("/products", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    return Results.Ok(products);
});

app.Run();

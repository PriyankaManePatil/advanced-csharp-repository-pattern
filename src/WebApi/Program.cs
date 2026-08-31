using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddScoped<IProductService, ProductService>();
// .NET 10 provides first-party OpenAPI generation; no third-party Swagger generator is required.
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseExceptionHandler();
// The JSON document is useful for learning, client generation and contract inspection.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

var products = app.MapGroup("/api/products").WithTags("Products");
products.MapGet("/", async (IProductService service, CancellationToken ct) => Results.Ok(await service.GetAllAsync(ct)));
products.MapGet("/{id:int}", async (int id, IProductService service, CancellationToken ct) =>
    await service.GetByIdAsync(id, ct) is { } product ? Results.Ok(product) : Results.NotFound());
products.MapPost("/", async (SaveProductRequest request, IProductService service, CancellationToken ct) =>
{
    var created = await service.CreateAsync(request, ct);
    return Results.Created($"/api/products/{created.ProductId}", created);
});
products.MapPut("/{id:int}", async (int id, SaveProductRequest request, IProductService service, CancellationToken ct) =>
    await service.UpdateAsync(id, request, ct) ? Results.NoContent() : Results.NotFound());
products.MapDelete("/{id:int}", async (int id, IProductService service, CancellationToken ct) =>
    await service.DeleteAsync(id, ct) ? Results.NoContent() : Results.NotFound());

app.MapHealthChecks("/health");

app.Run();

public partial class Program;

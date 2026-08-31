using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>Owns Infrastructure-layer registrations so the API composition root stays concise.</summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers one scoped DbContext, repository implementations and Unit of Work per request.
    /// Scoped lifetimes are important because all writes in one business operation must share the
    /// same change tracker before <see cref="IUnitOfWork"/> commits them.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
        .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductCatalogue"))
        .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
        .AddScoped(typeof(IReadRepository<>), typeof(ReadOnlyRepository<>))
        .AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<IUnitOfWork, EfUnitOfWork>();
}

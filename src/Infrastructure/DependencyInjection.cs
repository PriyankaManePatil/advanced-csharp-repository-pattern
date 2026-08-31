using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
        .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductCatalogue"))
        .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
        .AddScoped(typeof(IReadRepository<>), typeof(ReadOnlyRepository<>))
        .AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<IUnitOfWork, EfUnitOfWork>();
}

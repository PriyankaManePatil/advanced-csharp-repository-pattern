using Core.Interfaces;

namespace Infrastructure;

/// <summary>Adapts EF Core's built-in Unit of Work so the application layer stays persistence-agnostic.</summary>
public sealed class EfUnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);
}

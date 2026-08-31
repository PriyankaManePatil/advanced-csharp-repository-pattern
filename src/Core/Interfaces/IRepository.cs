using Core.Entities;

namespace Core.Interfaces;

/// <summary>
/// Full generic repository composed from separate read and write contracts.
/// Prefer an aggregate-specific interface when the domain needs meaningful, specialised queries.
/// </summary>
public interface IRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity>
    where TEntity : class, IEntity;

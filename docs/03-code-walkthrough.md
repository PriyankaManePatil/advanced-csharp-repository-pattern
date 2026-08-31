# 3. Code Walkthrough

## Core

- `IEntity` provides the shared key required by generic repositories.
- `Product` is the example aggregate root.
- `IReadRepository<T>` and `IWriteRepository<T>` demonstrate interface segregation.
- `IRepository<T>` composes both contracts.
- `IProductRepository` is the preferred application-facing, aggregate-specific abstraction.
- `ISpecification<T>` stores query intent as expressions without referencing EF Core.
- `IUnitOfWork` defines the explicit commit boundary.

Core has no EF Core or ASP.NET Core dependency.

## Application

`ProductService` validates input, maps entities to DTOs and commits successful commands through
`IUnitOfWork`. It never receives `DbContext` and never returns `IQueryable`.

## Infrastructure

- `ReadOnlyRepository<T>` supplies no-tracking queries.
- `EfRepository<T>` supplies generic query and staged write mechanics.
- `ProductRepository` inherits mechanics and adds product language.
- `SpecificationEvaluator` translates specifications into EF Core LINQ.
- `EfUnitOfWork` delegates the single commit to `AppDbContext`.
- `CachedReadRepository<T>` and `LoggingRepositoryDecorator<T>` demonstrate composition.
- `InMemoryRepository<T>` is a test double, not a relational emulator.

## WebApi

The Minimal API converts HTTP requests into application-service calls. It owns HTTP status codes and Problem
Details, while business validation remains in the service. `/openapi/v1.json` is exposed in Development.

## Why writes do not call SaveChanges

If each repository saves independently, an operation involving inventory, payment and order repositories can
partially commit. Staging changes and calling the Unit of Work once provides one transaction boundary.

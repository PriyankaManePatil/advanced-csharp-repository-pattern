# 11. File-by-File Learning Reference

## Recommended reading order

| Order | File/folder | Why it exists | What to notice |
|---:|---|---|---|
| 1 | `Core/Entities/Product.cs` | Example aggregate root | Entity contains data, not EF query code |
| 2 | `Core/Interfaces/IReadRepository.cs` | Narrow query capability | Interface Segregation Principle |
| 3 | `Core/Interfaces/IWriteRepository.cs` | Narrow command capability | Writes stage changes rather than commit |
| 4 | `Core/Interfaces/IRepository.cs` | Composed generic contract | Reuse without duplicating members |
| 5 | `Core/Interfaces/IProductRepository.cs` | Domain-specific boundary | Generic mechanics plus product language |
| 6 | `Core/Specifications/*` | Reusable query intent | Core expressions contain no EF dependency |
| 7 | `Infrastructure/Repositories/EfRepository.cs` | Generic EF mechanics | `AsNoTracking`, async calls, staged writes |
| 8 | `Infrastructure/Repositories/ProductRepository.cs` | Specific implementation | Delegates range logic to a specification |
| 9 | `Infrastructure/EfUnitOfWork.cs` | Explicit commit adapter | `DbContext.SaveChangesAsync` is called once |
| 10 | `Application/Services/ProductService.cs` | Use-case orchestration | Validation, mapping and transaction boundary |
| 11 | `Infrastructure/DependencyInjection.cs` | Runtime composition | Core/Application do not construct Infrastructure |
| 12 | `WebApi/Program.cs` | HTTP adapter | Status-code mapping and cancellation flow |
| 13 | `tests/UnitTests` | Isolated behaviour tests | Repositories and Unit of Work are mocked |
| 14 | `tests/IntegrationTests` | Component interaction tests | Real ASP.NET/EF pipeline is exercised |

## Alternative implementations

- `ReadOnlyRepository<T>`: exposes query capability only.
- `CachedReadRepository<T>`: decorator demonstrating caching structure.
- `LoggingRepositoryDecorator<T>`: decorator demonstrating cross-cutting diagnostics.
- `InMemoryRepository<T>`: lightweight test double, not a relational database emulator.
- `SpecificationEvaluator`: only Infrastructure knows how specifications become EF queries.

## Dependency rule to remember

```text
WebApi -> Application -> Core
WebApi -> Infrastructure -> Core
Core -> nothing
```

If Core starts referencing EF Core, ASP.NET Core or Infrastructure, the dependency direction has been broken.

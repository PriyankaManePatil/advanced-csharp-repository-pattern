# 8. Glossary

| Term | Meaning in this project |
|---|---|
| Aggregate | A consistency boundary containing entities/value objects changed together |
| Aggregate root | The entity through which an aggregate is loaded and modified; `Product` is the sample root |
| Repository | Domain-facing abstraction for loading and staging persistence changes |
| Generic repository | Reusable CRUD mechanics parameterised by entity type |
| Specific repository | Aggregate-oriented contract containing domain-specific query names |
| Unit of Work | Coordinates one commit across changes made during a business operation |
| Specification | Object containing reusable filter/order query intent |
| Decorator | Wrapper implementing the same contract to add logging, caching or another policy |
| DTO | Data Transfer Object used to cross a boundary without exposing persistence entities |
| DbContext | EF Core session that tracks entities and implements Unit of Work behaviour |
| DbSet | EF Core collection/query entry point for one entity type |
| Change tracking | EF Core mechanism that detects inserted, modified and deleted entities |
| AsNoTracking | Read optimisation that excludes results from the change tracker |
| Dependency inversion | High-level code depends on abstractions rather than EF implementations |
| Dependency injection | Runtime composition of services and implementations in `Program.cs`/registration extensions |
| IQueryable | Deferred provider-specific query; intentionally kept inside Infrastructure |
| Deferred execution | Query runs when enumerated, not necessarily when constructed |
| CancellationToken | Cooperative signal used to cancel asynchronous work |
| CQRS | Separation of command/write and query/read responsibilities and, when justified, models |
| Test double | Replacement used by a test; the sample `InMemoryRepository<T>` is one example |
| Integration test | Test exercising multiple real components, such as repository plus EF provider |
| Optimistic concurrency | Detecting conflicting updates without locking a record for the whole operation |

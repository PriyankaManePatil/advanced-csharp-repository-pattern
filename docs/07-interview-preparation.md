# 7. Repository Pattern Interview Preparation

Use these as talking points rather than memorised definitions. A strong interview answer explains the trade-off
and connects it to a concrete class in this repository.

## Core questions and model answers

### 1. What is Repository Pattern?

Repository Pattern places a collection-like abstraction between application/domain logic and persistence. It
centralises data-access rules and lets use cases request domain entities without knowing EF Core, SQL or table
details. In this project, `IProductRepository` is the abstraction and `ProductRepository` is its EF implementation.

### 2. Why use a repository when EF Core already has DbSet?

`DbSet<T>` already behaves like a repository and `DbContext` like a Unit of Work. A custom repository is useful
when it adds domain language, protects aggregate boundaries, centralises specifications, or permits a meaningful
alternative implementation. A wrapper that only renames every `DbSet` method adds little value.

### 3. Generic versus specific repository?

A generic repository removes repeated mechanics such as `GetById`, `Add` and `Delete`. A specific repository
expresses domain queries such as `GetByPriceRangeAsync`. This project combines them: `ProductRepository` inherits
`EfRepository<Product>` but exposes the aggregate-specific `IProductRepository` contract.

### 4. What is Unit of Work?

Unit of Work tracks changes made during one business operation and commits them as one transaction. Repository
methods here stage changes; `ProductService` calls `IUnitOfWork.SaveChangesAsync` once after a successful command.

### 5. Why should a repository not return IQueryable?

Returning `IQueryable` leaks the ORM and query provider outside Infrastructure. Callers can accidentally create
slow or unsupported queries, and testing becomes coupled to EF. Named methods and specifications keep the
boundary explicit.

### 6. What does AsNoTracking do?

It tells EF Core not to keep read entities in its change tracker. This reduces memory and tracking overhead when
the result is only being displayed. It should not be used when the same entity instance will be modified and
saved through change tracking.

### 7. What is Specification Pattern?

A specification packages reusable filtering and ordering rules as an object. `ProductsInPriceRangeSpecification`
contains domain query intent; `SpecificationEvaluator` translates it into an EF Core query.

### 8. Why split read and write interfaces?

Interface segregation gives a consumer only the capability it needs. A reporting service can depend on
`IReadRepository<T>` and cannot accidentally call `DeleteAsync`. This resembles CQRS at the contract level but
does not require separate databases or models.

### 9. How do decorators help repositories?

A decorator implements the same contract, wraps another implementation and adds cross-cutting behaviour.
`LoggingRepositoryDecorator<T>` adds logs without modifying EF code; `CachedReadRepository<T>` adds read caching.

### 10. What is the caching danger?

Stale data. A real cached repository needs explicit expiration and invalidation rules after writes. The sample
shows composition, while the documentation warns that it is not automatically safe to register in production.

### 11. How should repositories be tested?

Unit-test application decisions using mocked contracts. Integration-test the real repository with its database
provider. EF In-Memory is convenient for this reference but does not verify SQL translation, foreign keys,
transactions or isolation, so production projects should also test against their actual provider.

### 12. Should every table have a repository?

No. Repositories normally represent aggregate roots and consistency boundaries, not tables. Child entities are
usually loaded and saved through their aggregate root.

### 13. Where should validation occur?

Request-shape validation can occur at the API boundary; use-case and business validation belongs in the
application/domain layers. Persistence constraints remain in database configuration. `ProductService` normalises
names and rejects invalid prices before calling the repository.

### 14. How are not-found cases represented here?

Queries return nullable results. Update and delete return `bool`. The application/API translates those results
to HTTP `404` without using exceptions for an expected condition.

### 15. Why pass CancellationToken?

It allows abandoned HTTP requests and shutdown signals to stop database work. The token is passed from the
Minimal API through the service and repository to EF Core async operations.

## Scenario questions

- **A query joins ten tables and returns a dashboard DTO.** Prefer a dedicated query service or Dapper projection;
  do not force it through an aggregate repository.
- **An order operation updates order and inventory.** Stage both changes through repositories sharing one context,
  then commit once with Unit of Work.
- **The same lookup runs thousands of times.** Consider a cached read decorator after defining staleness and
  invalidation rules.
- **The application only has basic CRUD screens.** Direct `DbContext` may be simpler than custom repositories.
- **The business needs a complete history and temporal reconstruction.** Consider event sourcing; an ordinary
  relational repository is not an event store.

## Questions to ask the interviewer

1. What are the aggregate and transaction boundaries?
2. Are reads and writes served by the same model and store?
3. Which queries cause current performance problems?
4. Is persistence substitution a real requirement or only a theoretical one?
5. How are integration tests run against the production database engine?

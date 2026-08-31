# 9. Hands-on Exercises

These exercises are optional; the checked-in solution remains a compact reference. Complete them in order on a
separate branch.

## Beginner

### Exercise 1: Trace a read

Set breakpoints (or follow the code statically) from `GET /api/products/{id}` to `ProductService`,
`IProductRepository`, `ProductRepository`, `EfRepository` and `AppDbContext`.

**Expected learning:** dependency direction and the difference between contract and implementation.

### Exercise 2: Add a DTO field

Add `Description` to the entity, request DTO, response DTO, EF configuration, mapping and tests.

**Expected learning:** why boundary models and persistence models change independently.

### Exercise 3: Add a repository query

Add `GetByNameAsync` to `IProductRepository` and implement it without returning `IQueryable`.

**Expected learning:** domain-oriented repository APIs.

## Intermediate

### Exercise 4: Add paging to a specification

Extend specifications with `Skip` and `Take`, update the evaluator, and test stable ordering.

**Expected learning:** reusable query composition and why paging requires deterministic ordering.

### Exercise 5: Register a logging decorator

Compose `LoggingRepositoryDecorator<T>` around the generic repository and verify that behaviour is unchanged.

**Expected learning:** Open/Closed Principle and DI composition.

### Exercise 6: Design cache invalidation

Define which cache keys must be cleared after create, update and delete. Add expiry and tests.

**Expected learning:** caching is a consistency decision, not only a performance feature.

## Advanced

### Exercise 7: Add SQL Server provider tests

Add a separate integration-test configuration using SQL Server/Azure SQL and migrations. Do not replace fast unit
tests; add provider verification.

**Expected learning:** EF In-Memory cannot validate relational semantics.

### Exercise 8: Implement optimistic concurrency

Add a row-version property, configure it as a concurrency token, handle `DbUpdateConcurrencyException`, and
return an appropriate API conflict response.

**Expected learning:** lost-update prevention and HTTP `409 Conflict`.

### Exercise 9: Add an Order aggregate

Create `Order`, `OrderLine` and `IOrderRepository`. Save order and inventory changes through one Unit of Work.

**Expected learning:** repositories are aggregate-oriented, not table-oriented.

## Review checklist

- Did the change preserve dependency direction?
- Did any `IQueryable` escape Infrastructure?
- Does one business operation commit once?
- Are cancellation tokens passed through?
- Are both success and failure paths tested?
- Does the documentation explain the new trade-off?

# 2. Repository Pattern Catalogue

There is no finite official list of “all repository patterns.” Repository is one pattern with many variants.
This catalogue covers the variants most often encountered in modern .NET systems.

| Variant | Included | Use when | Avoid when |
|---|---:|---|---|
| Aggregate-specific repository | Yes | Domain queries need meaningful names | The model is simple CRUD with no domain boundary |
| Generic repository | Yes | Many simple entities share safe CRUD mechanics | It becomes a lowest-common-denominator abstraction |
| Read-only repository | Yes | Reporting/lookups must not write | Consumers genuinely require commands |
| Read/write segregation | Yes | Separate query and command capabilities improve clarity | It adds interfaces without a real boundary |
| Specification repository | Yes | Filters/order rules must be reusable and testable | A one-off query is clearer inline in a specific repository |
| Unit of Work | Yes | Several changes must commit atomically | Every method saves independently |
| Decorator repository | Yes | Logging, metrics or policies are cross-cutting | Decorators hide important domain behaviour |
| Cached read repository | Yes | Reads are repeated and staleness is acceptable | Strong consistency is required or invalidation is unclear |
| In-memory test double | Yes | Fast isolated tests need the same contract | Replacing real provider integration tests |
| Direct `DbContext` | Documented | Small vertical slices already treat EF Core as the abstraction | Persistence independence or domain-specific queries matter |
| Dapper/SQL repository | Documented | Query performance/control requires explicit SQL | A second data stack adds no measurable benefit |
| CQRS query service | Documented | Read models differ greatly from write aggregates | CRUD screens do not need separate models |
| Event-sourced repository | Documented | Aggregates are rebuilt from event streams | Ordinary relational CRUD is sufficient |
| Composite repository | Documented | One logical view combines multiple data sources | Distributed consistency cannot be handled explicitly |

## Why every variant is not registered simultaneously

The compiled classes demonstrate their shapes, but Dependency Injection registers only the abstractions used
by the API. Registering generic, cached, logging and specific implementations in one chain requires an explicit
composition decision. A learning project should show choices without pretending all choices belong in every app.

## Data technology variants

- **EF Core:** best default for aggregate persistence and change tracking.
- **Dapper:** implement the same specific contract with parameterised SQL for read-heavy or tuned queries.
- **Azure Table/Cosmos DB:** design contracts around partition and aggregate access; do not copy relational APIs.
- **Event store:** repository loads an event stream and saves new events with optimistic concurrency.

The contract should reflect domain access patterns. Changing only the implementation is safe only when the
semantic guarantees—transactions, consistency, ordering and concurrency—remain compatible.

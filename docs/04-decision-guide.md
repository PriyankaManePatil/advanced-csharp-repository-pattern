# 4. Decision Guide

| Situation | Recommended starting point |
|---|---|
| Simple CRUD application using only EF Core | Direct `DbContext` or a thin specific repository |
| Rich domain model with aggregate rules | One specific repository per aggregate root + Unit of Work |
| Many entities share basic persistence operations | Generic base implementation behind specific interfaces |
| Repeated complex filters | Specification pattern |
| Read-only reporting | Read-only repository or dedicated query service |
| Read and write models are fundamentally different | CQRS query services + aggregate repositories |
| Hot, stable reads | Cached read decorator with explicit invalidation policy |
| Cross-cutting diagnostics | Logging/metrics decorator |
| Tuned SQL is required | Dapper implementation of a query-specific interface |
| Full audit history is the source of truth | Event-sourced repository |

## Practical selection questions

1. What is the aggregate boundary?
2. Does the caller need commands, queries or both?
3. Must multiple changes commit atomically?
4. Is the abstraction hiding technology or merely renaming `DbSet`?
5. Are consistency and concurrency guarantees written down?
6. Can the important query be expressed with a domain-oriented method or specification?

Start with the smallest meaningful abstraction. Add a variant when a demonstrated requirement justifies it.

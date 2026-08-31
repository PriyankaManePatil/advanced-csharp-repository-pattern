# 6. Pitfalls and Anti-patterns

## Returning IQueryable

Returning `IQueryable` leaks the ORM boundary, lets callers build unreviewed queries and makes exception timing
unpredictable. Prefer named methods, projections or specifications.

## One repository per table

Repositories model aggregate persistence, not schema structure. Table-based repositories often produce
anemic services and transactions spread across unrelated objects.

## Saving inside every method

This defeats Unit of Work and can partially persist a business operation. Stage changes, then commit once.

## Generic repository with unlimited operations

An abstraction containing includes, raw SQL, arbitrary expressions, paging, joins and provider flags simply
recreates `DbContext` poorly. Keep generic contracts small and add specific domain operations.

## Assuming in-memory means database-correct

EF Core In-Memory does not reproduce relational constraints, SQL translation, isolation or transaction
behaviour. It is useful for demonstrations, not proof of SQL Server correctness.

## Caching without invalidation

A cached repository needs ownership of expiry and invalidation. The sample decorator caches reads only to show
structure; production composition must invalidate keys after writes or tolerate bounded staleness.

## Hiding important costs

Repository names should reveal expensive operations where possible. Avoid a harmless-looking property that
loads thousands of records or triggers multiple remote calls.

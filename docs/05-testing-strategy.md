# 5. Testing Strategy

## Unit tests

Mock repository and Unit of Work contracts when testing application decisions: validation, mapping, not-found
behaviour and whether successful commands commit. Do not mock EF Core LINQ internals.

## Repository integration tests

Exercise the repository and Unit of Work together. The current In-Memory provider keeps CI self-contained but
does not validate SQL translation, constraints, transactions or provider-specific behaviour. A production
solution should add tests against the chosen provider—often SQL Server in a disposable container/database.

## API integration tests

`WebApplicationFactory` verifies dependency registration, serialization, routing and HTTP status codes through
the real ASP.NET Core pipeline.

## What to verify

- Reads use expected filters and ordering.
- Missing updates/deletes return `false` and do not commit.
- Successful commands call Unit of Work once.
- Cancellation tokens reach async persistence calls.
- Specifications reject invalid parameters.
- Decorator behaviour does not change repository semantics.

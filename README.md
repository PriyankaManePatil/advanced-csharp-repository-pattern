# Advanced C# Repository Pattern

[![.NET CI](https://github.com/PriyankaManePatil/advanced-csharp-repository-pattern/actions/workflows/dotnet.yml/badge.svg)](https://github.com/PriyankaManePatil/advanced-csharp-repository-pattern/actions/workflows/dotnet.yml)

A buildable .NET 8 reference implementation of Repository Pattern and layered architecture. It demonstrates dependency inversion, an application service layer, Entity Framework Core, Minimal APIs, validation, error handling, automated tests and CI.

> This project uses EF Core's in-memory provider for learning and architectural demonstration. Use a durable database provider and production security/observability controls for a real deployment.

## Architecture

```text
WebApi -> Application -> Core
   |                       ^
   +----> Infrastructure --+
```

- **Core**: domain entities and persistence abstractions
- **Application**: DTOs, validation and use-case orchestration
- **Infrastructure**: EF Core context and repository implementation
- **WebApi**: Minimal API endpoints, dependency injection, Swagger and exception handling
- **UnitTests**: isolated application service tests
- **IntegrationTests**: repository and HTTP endpoint tests

![Architecture diagram](docs/architecture-diagram.png)

## Technology

- .NET 8
- ASP.NET Core Minimal APIs
- Entity Framework Core In-Memory
- xUnit, Moq and WebApplicationFactory
- Coverlet code coverage
- GitHub Actions

## Run locally

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), then run:

```bash
dotnet restore AdvancedRepositoryPattern.sln
dotnet build AdvancedRepositoryPattern.sln
dotnet run --project src/WebApi/WebApi.csproj
```

In Development, Swagger is available at `http://localhost:<port>/swagger`. The exact port is printed by `dotnet run`.

## API

| Method | Route | Result |
|---|---|---|
| GET | `/api/products` | All products |
| GET | `/api/products/{id}` | Product or `404` |
| POST | `/api/products` | Created product and `201` |
| PUT | `/api/products/{id}` | `204` or `404` |
| DELETE | `/api/products/{id}` | `204` or `404` |
| GET | `/health` | Application health |

Example request:

```json
{
  "name": "Mechanical Keyboard",
  "price": 99.50
}
```

## Test and coverage

```bash
dotnet test AdvancedRepositoryPattern.sln --collect:"XPlat Code Coverage" --results-directory TestResults
```

Tests cover service validation, CRUD repository behaviour, missing records and HTTP CRUD status codes. CI executes restore, build and tests for every pull request and push to `main`.

## Design decisions

- Read queries use `AsNoTracking`.
- Repository operations accept cancellation tokens.
- Update and delete return `bool` to represent not-found results without exceptions.
- API contracts use DTOs rather than exposing EF Core entities.
- Standard `ILogger<T>` is preferred over a custom logging wrapper.
- An in-memory provider keeps the sample self-contained; it does not reproduce every relational-database behaviour.

## License

Licensed under the MIT License.

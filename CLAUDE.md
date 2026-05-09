# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
# Build the solution
dotnet build ImageGallery.sln

# Run with full Aspire orchestration (recommended — starts SQL Server, Keycloak, API)
dotnet run --project ImageGallery.AppHost/ImageGallery.AppHost.csproj

# Run the API directly (requires SQL Server and Keycloak already running)
dotnet run --project ImageGallery.API/ImageGallery.API.csproj
```

No test projects exist in this solution.

### EF Core migrations

```bash
dotnet ef migrations add <Name> --project ImageGallery.Infrastructure --startup-project ImageGallery.API
dotnet ef database update --project ImageGallery.Infrastructure --startup-project ImageGallery.API
```

## Architecture

Six projects with a layered + CQRS-inspired structure:

| Project | Role |
|---|---|
| `ImageGallery.API` | HTTP layer — FastEndpoints, JWT auth, CORS |
| `ImageGallery.Core` | Business logic — command handlers, domain services |
| `ImageGallery.Infrastructure` | Data — EF Core DbContext, repositories, migrations |
| `ImageGallery.Shared` | Cross-cutting — `BaseEntity`, `BaseEndpoint`, `IAppRepository<T>` |
| `ImageGallery.AppHost` | .NET Aspire orchestration for local dev |
| `ImageGallery.ServiceDefaults` | OpenTelemetry, health checks, service discovery |

### Request flow

```
HTTP Request
  → FastEndpoints endpoint (API/Endpoints/{Feature}/{Action}/)
  → FluentValidation pre-processor (validates before handler runs)
  → endpoint calls ICommandHandler via FastEndpoints' command bus
  → Core handler → IAppRepository<T> → EF Core (SQL Server)
  → FluentResults Result<T> returned up the chain
```

### Key patterns

**Endpoints** inherit `BaseEndpoint<TRequest, TResponse>` (from Shared). Each feature folder contains:
- `{Action}.cs` — endpoint definition with `Summary()` for Swagger
- `{Action}.Request.cs` — request/response DTOs
- `{Action}.Validations.cs` — FluentValidation rules

**Commands** are records: `record Command : ICommand<Result<T>>`. Handlers are classes: `class Handler : ICommandHandler<Command, Result<T>>`. Handlers are registered via Scrutor assembly scanning in `Program.cs` and manually in `Core/DependencyInjection.cs`.

**Repositories** use the generic `AppRepository<TEntity>` (Infrastructure) registered as `IAppRepository<TEntity>` (Shared). All entities inherit `BaseEntity` (Guid Id, CreatedAt, ModifiedAt).

**Error handling** uses FluentResults — business logic never throws; endpoints check `result.IsFailed`.

### Domain entities

`CategoryEntity` → `ImageEntity` (1:N) → `CommentEntity` (1:N)  
`ServiceEntity` (standalone)  
`AboutUsEntity` → `EmployeeEntity` + `ContactEntity` (1:N each)

Entity constraints: Title max 50 chars, Description max 500 chars (enforced in EF config and FluentValidation).

## Local dev configuration

**Aspire** provisions automatically:
- SQL Server on port `1433` (sa / `Aa123456`, database: `ImageGallery`)
- Keycloak on port `8080`, realm: `ImageGallery`, test user: `test1` / `Pass123$`

**API** listens on `http://0.0.0.0:7113`.  
**CORS** allows `http://localhost:5173` (frontend).  
**JWT authority**: `http://localhost:8080/realms/ImageGallery`

> Keycloak authority URL is hardcoded in `Program.cs` — TODO to move it to appsettings.
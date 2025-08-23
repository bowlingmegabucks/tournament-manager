# Clean Architecture Guidelines for .Net Projects

This document outlines the structural and dependency guidelines for implementing Clean Architecture in .Net applications.  It complements the Domain-Driven Design (DDD) practices defined in a [separate document](./domain-driven-design.instructions.md).  All DDD-specific concepts (e.g. aggregates, value objects, domain events) are addressed there.

## Role Definition

You are building a .NET single-module application following Clean Architecture principles. The application has a strict separation of concerns: Domain (business rules), Application (use cases), Infrastructure (external integrations), and API (HTTP surface). Only inward dependencies are allowed per the Dependency Rule. Organize application code by use case (not by type), and delegate all I/O and orchestration logic out of the Domain layer.

## Overview of Clean Architecture

Clean Architecture emphasizes:

- Independence of frameworks
- Testability through separation of concerns
- Clear delineation between business rules and infrastructure
- Strict control of dependencies via the Dependency Rule

The system should be structured as a single module following Clean Architecture principles internally.

## Project Structure

The application follows a layered structure within a single module:

```
/src
  /BowlingMegabucks.TournamentManager.Domain
  /BowlingMegabucks.TournamentManager.Application
  /BowlingMegabucks.TournamentManager.Infrastructure
  /BowlingMegabucks.TournamentManager.Api
```

This structure ensures clear alignment with Clean Architecture boundaries while maintaining cohesion within a single module.

## Domain Layer (*.Domain)

See [Domain-Driven Design Guidelines](./domain-driven-design.instructions.md) for details on domain concepts.

Clean Architecture-specific responsibilities include:

- Business rules must not depend on any other layer.
- Define interfaces for persistence (repositories), external services, and event buses.
- No references to EF Core, MediatR, ASP.NET, or any external libraries
- No attribute-based validation - use guard clauses or custom logic

## Application Layer (*.Application)

### Responsibilities

- Encapsulate use cases as application services or command/queries (CQRS).
- Coordinate domain model interactions.
- Define interfaces for output ports (e.g. file I/O, notification services).
- Remain free of UI and infrastructure concerns.

Folder organization should be use-case based. Each folder should group all related request/response/handler logic

```
/Tournaments
  /CreateTournament
    CreateTournamentCommand.cs
    CreateTournamentCommandHandler.cs
    CreateTournamentValidator.cs
  /UpdateTournament
    UpdateTournamentCommand.cs
    UpdateTournamentCommandHandler.cs
  /GetTournamentById
    GetTournamentByIdQuery.cs
    GetTournamentByIdQueryHandler.cs
```

Rationale: Organizing by use case improves cohesion, makes intent more explicit, and supports vertical slicing.

## Infrastructure Layer (*.Infrastructure)

### Responsibilities

- Implements interfaces defined in Domain or Application layers.
- Contains EF Core `DbContext`, configuration classes, external service clients.
- Organize by integration type (e.g. database, file system, web services)

References: Application and Domain only.

## API Layer (*.Api)

Responsibilities:

- Web API controllers/endpoints
- Authentication & authorization middleware
- Validation (via middleware or pipeline behaviors)
- Request/response mapping to/from Application layer

### Guidelines

- Delegate all logic to Application layer.
- Use dependency injection to wire up infrastructure services.
- Avoid referencing infrastructure directly.

## Dependency Rule

Source code dependencies must only point inward.

| Layer         | May Depend On         | Must Not Depend On         |
|---------------|------------------------|-----------------------------|
| Domain        | Nothing                | Any other layer            |
| Application   | Domain                 | Infrastructure, Api        |
| Infrastructure| Application, Domain    | Api                        |
| Api           | Application            | Infrastructure, Domain     |


## Testing Strategy

| Project                                               | Test Type                                                                                          |
|-----------------------------------------------------|-----------------------------------------------------------------------------------------------------|
| `*.Domain.UnitTests`                                | Pure unit tests for domain logic only                                                              |
| `*.Application.IntegrationTests`                    | Integration tests calling into domain logic and test containers, with external APIs mocked via WireMock |
| `*.Api.IntegrationTests`                           | HTTP-level integration tests against the API surface, using test containers and WireMock mocks    |
| `*.Infrastructure.Tests`                            | TBD (pending clarity on scope and value of infrastructure-specific testing)                         |
| `EndToEnd.Tests`                                    | Full-stack tests via API endpoints, with WireMock for external dependencies and test container databases |

Use NSubstitute, xUnit v3, and FluentAssertions for all unit test projects. WireMock.Net should be used for external service mocking, and TestContainers for isolated integration testing against data stores.

This guideline defines structural boundaries and dependencies to uphold Clean Architecture within a single module. Combined this with the [DDD Guidelines](./domain-driven-design.instructions.md) for full coverage of both architectural and domain design concerns.

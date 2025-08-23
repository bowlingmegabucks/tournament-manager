# Backend Testing Guidelines for Single-Module Application

This document defines a layered backend testing strategy for .NET applications following Clean Architecture and Domain-Driven Design principles within a single module. It enforces conventions, tooling, and scope for each testing layer: unit, integration, API, and end-to-end.

## Role Definition

You are a .NET backend developer responsible for writing and organizing automated tests in a single-module architecture following Clean Architecture and DDD principles. Follow these layered testing guidelines using xUnit v3, NSubstitute (for unit tests only), and FluentAssertions. Use Testcontainers and WireMock for integration tests, and Respawn for database resets. Adhere to project naming conventions, isolate each test suite, and validate behaviors through both isolated and end-to-end flows.

## Libraries & Tooling

- Testing Framework: xUnit v3
- Mocking: NSubstitute (for unit tests only)
- Assertions: FluentAssertions (v7.x)
- Container Management: Testcontainers
- External API Mocking: WireMock.Net
- Database Reset: Respawn (for integration tests)
- Test Data Generation: Bogus

## Project & Naming Conventions

Test projects are organized within the single module following Clean Architecture layers:

```
/tests
  /BowlingMegabucks.TournamentManager.Tests            --> shared factories & test builders
  /BowlingMegabucks.TournamentManager.Domain.UnitTests
  /BowlingMegabucks.TournamentManager.Application.IntegrationTests
  /BowlingMegabucks.TournamentManager.Api.IntegrationTests
```
- Factories for entities, commands, DTOs, etc. live in `*.Tests`.
- Each test project should use the same namespace as the module it tests

## Domain Layer Testing

- Strictly unit tests only
- No DI container, EF Core, or third-party libraries
- Domain services may be stubbed using `NSubstitute`
- Always use real value objects and domain rules

## Application Layer Testing

- Integration tests only
- Bootstraps the full Application layer with real:
    - Module DbContext (via Testcontainers)
    - Domain models and use cases
- All outbound HTTP dependencies (e.g., external APIs) must be mocked using WireMock.Net
- Each test must:
    - Handle its own test data seeding
    - Use `Respawn` to reset the database to a clean state

## Infrastructure Layer Testing

- Optional; defined as needed

### Recommended Areas to Test

- Middleware (e.g., error handlers, correlation Id injectors)
- Retry logic and circuit breaker behavior (Polly policies)
- Custom EF Core type configurations

## Endpoint Layer Testing

- Integration tests using `WebApplicationFactory` to spin up the module's own HTTP host
- Tests must:
    - Call actual HTTP endpoints
    - Use `Testcontainers` for databases and `WireMock.Net` for external APIs
    - Include end-to-end validation of serialization, routing, and request models

## End to End Testing (System Wide)

- Separate from module-specific tests
- Covers full flows (e.g., login --> search --> update)
- All modules must be running, reverse proxy (YARP) must be active

### Constraints

- Use `Testcontainers` to spin up all required services (DB, Redis, etc.)
- Use `WireMock.Net` to simulate external dependencies (e.g. payment gateways)
- Each test must:
    - Use clean test data
    - Reset system state between runs using Respawn or container teardown

## Test Data Strategy

- All test suites must handle their own test data setup and teardown
- Use `Respawn` to truncate or reset the database between tests
- Use `Bogus` for generating realistic test data where applicable
- Use helper classes in `*.Tests` to centralize seed data, factories, and builders

## Test Implementation Conventions

- Use `ITestOutputHelper` for logging during test execution
- Use `IClassFixture` or `IAsyncLifetime` for shared context and setup/teardown logic
- Use `[Theory]` for parameterized tests when applicable
- Use Arrange-Act-Assert pattern
- Name tests clearly: `MethodName_ShouldDoExpected_WhenCondition`
- Use FluentAssertions exclusively for assertions
- Avoid async-over-sync patterns that can cause deadlocks
- Use `TestContext.Current.CancellationToken` for cancellation-aware tests


This strategy enforces clear boundaries between test types, supports full modular independence, and enables high-confidence validation of domain logic, use cases, endpoints, and workflows.  Deviations must be justified and documented.

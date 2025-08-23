# Cross-Cutting Concerns Instructions for Single-Module Application

This document defines architecture-aligned conventions for handling cross-cutting concerns in a .NET single-module application following [Clean Architecture](./clean-architecture.instructions.md) and [Domain-Driven Design](./domain-driven-design.instructions.md). These practices are structured to support production-scale systems, including observability, resilience, and traceability.

## Role Definition

You are designing or reviewing cross-cutting concerns in a .NET single-module application. Apply the project's official conventions for logging, validation, exception handling, telemetry, caching, domain events, and resilience. Use `Microsoft.Extensions.Logging` (with OpenTelemetry), `FluentValidation`, and ProblemDetails (RFC7807). Validation should run through a custom pipeline. All domain events should be dispatched explicitly, and background processing must use the outbox pattern with `Hangfire`.

## Logging

### Libraries & Tooling

- Use `Microsoft.Extensions.Logging`
- Structured logging is required.
- Enrich logs with:
  - `CorrelationId`
  - `RequestId`
  - `UserId` (if available)
  - `ModuleName` (to identify the module generating the log)

### Output Targets

- Local: Aspire Dashboard
- Production: Azure Application Insights (via OpenTelemetry Exporter)


## Validation

### Libraries

- Use `FluentValidation` for:
    - API request model validation
    - Command-level validation in the Application layer

### Execution

- Validations are triggered by a custom pipeline behavior (not via MediatR)
- Business rules are enforced in domain entities using the Result pattern.
- Guard clauses are used inside private constructors for invariants that must never be bypassed (developer safety net).

## Exception Handling

### Global Handling

- Use ASP.Net Core's built-in middleware for exception handling.
- Custom exception-to-response mapping logic must:
    - Return structured `ProblemDetails` as per [RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807)
    - Classify most domain exceptions as `4xx`
    - Use 5xx sparingly for non-client related errors

Example application/problem+json Response

```json
{
  "type": "https://httpstatuses.com/400",
  "title": "Validation Error",
  "status": 400,
  "detail": "One or more validation failures occurred.",
  "instance": "/api/members",
  "extensions": {
    "errors": {
      "FirstName": ["First name is required."],
      "Email": ["Email must be a valid address."]
    },
    "correlationId": "b6d7e8c3-d0e5-4c0e-a6f0-9c3a6bfa8b4f"
  }
}
```
## Domain Events

### Dispatching

- Domain events must be explicitly dispatched after `SaveChangesAsync`
- Use a custom mediator abstraction, not MediatR

### Handling

- In-process subscribers can respond immediately.
- For asynchronous/background processing, use Inbox/Outbox pattern
    - Persist outgoing events to an Outbox table
    - Use `Hangfire` to process events from outbox in background workers

## Caching

### Strategy

- Opt-in per use case: queries implement `ICacheableQuery` instead of `IQuery`
- Cache configuration:
    - Backed by .Net MemoryCache
    - Define a default policy (e.g. TTL = 5 minutes)
    - Allow per-query override

### Extensability

- System must support future upgrade to a distributed cache with minimal changes

## Telemetry & Tracing

### Libraries

- Use `OpenTelemetry` for:
    - Request tracing
    - Logging enrichment
    - Metrics emission

### Required

- All incoming HTTP requests must auto-generate and propagate a `CorrelationId`
- CorrelationId must flow through logs and outbound service calls

## Resilience (External Services)

### Libraries

- Use `Polly` for resilience policies

### Default Policies

- Retry with backoff for transient errors
- Circuit breaker enabled by default
- Timeouts must be configured per integration


This file governs cross-cutting behavior across all modules in the system.  Any deviation must be explicitly documented with justification.

# Persistence Guidelines for .NET Single-Module Application

This document outlines best practices for implementing persistence using Entity Framework Core and PostgreSQL in a single-module application following Clean Architecture and Domain-Driven Design principles. All conventions align with [Clean Architecture](./clean-architecture.instructions.md), [Domain-Driven Design](./domain-driven-design.instructions.md), and modern database design principles informed by **Database Systems: The Complete Book**.

## Role Definition

You are implementing persistence for a .NET single-module application using Entity Framework Core with PostgreSQL. Use Fluent API only (no data annotations). The application uses a single schema but organizes data by domain contexts within that schema. Entities use ULIDs prefixed by type name via the `StronglyTypedId` source generator. Design for future multi-tenancy by including optional TenantId and scoping support. Normalize schemas unless pragmatically necessary, and document any denormalization with justifications.

## ORM Strategy

### Primary ORM

- Use Entity Framework Core as the primary ORM.
- All entity configurations must be done via the Fluent API.
- Data annotations are not allowed.

### Value Objects

- All value objects should be mapped using EF Core's owned entity types.

### Complex Queries

- Use EF Core where possible
- For performance-critical queries, `Dapper` may be introduced on a case-by-case basis.
- Dapper-based read models must be well-separated and documented.

## Schema and Table Conventions

### Single Schema Approach

- The application uses a single PostgreSQL schema (`public` or custom named)
- Domain entities are organized logically within this schema but not physically separated
- Tables are prefixed by domain context for clarity (e.g., `tournament_`, `bowler_`, `score_`)

### Table Naming

- Use plural table names in snake_case
    - Example: `members`, `orders`, `products`, `tournament_results`

### Primary Keys

- Use ULID-based strongly-typed Ids via `StronglyTypedId` source generator.
- Each ID must be prefixed with the entity type
    - Example: `bwl_01H9HTG6KHX52VZK53K00Z5F6B`, `trn_01H9HTG6KHX52VZK53K00Z5F6C`

## Multi-Tenancy Considerations

While the application currently supports only a single tenant, all persistence design decisions should account for potential future multi-tenant requirements

### Entity Design

- Entities may include a `TenantId` property for future compatibility
- If included, unique indexes involving business identifiers should be scoped by `TenantId`

### Query Scoping

- Global query filters for `TenantId` may be added as needed to isolate tenant data
- A bypass mechanism (e.g. for admin views) should be considered when multi-tenancy is implemented

Multitenancy support is not currently required, but care should be taken to avoid assumptions that prevent its future introduction.

## Migrations

### Structure

- The application uses a single set of EF Core migrations managed in the Infrastructure project
- All domain entities share the same migration history and database schema evolution

### Execution

- Migrations must be automatically applied in non-production environments
- Production application must leverage CI/CD pipelines to apply migrations
- Use `dotnet ef` CLI commands for migration management

## DbContext Strategy

### Single DbContext

- The application defines a single `DbContext` (e.g., `TournamentManagerDbContext`)
- This context encompasses all domain entities within the single module

### Base Class

- No `BaseDbContext` unless explicitly justified
- Shared behavior should be duplicated unless a strong case is made for abstraction

## Referential Integrity

### Foreign Keys

- All foreign key relationships must be:
    - Explicitly declared via Fluent API
    - Indexed for performance and integrity

## Normalization & Denormalization

- Default to 3rd Normal Form (3NF)
- Avoid duplication unless:
    - There is a measurable performance bottleneck and
    - Alternative solutions (e.g., indexed views, read models) have been explored and ruled out

### Documentation Requirements

- Any denormalization must be documented with:
    - The alternatives considered
    - Why denormalization was necessary

## Data Seeding

- The application must not seed reference/config data via EF Core migrations
- If seeding is required, it must be handled:
    - External (e.g., SQL scripts, manual imports)
    - Or via application-level provisioning logic

## Summary

This guideline defines how persistence is handled consistently within the single-module application following Clean Architecture and DDD principles. The approach maintains logical separation of domain concepts while using a unified persistence strategy. Deviations must be justified with documented rationale.

# Domain-Driven Design (DDD) Instructions

This document defines principles, practices, and conventions to guide implementation of Domain-Driven Design (DDD) in .Net applications.  It is intended to be used by developers and tools (like GitHub Copilot) to ensure consistency and alignment with DDD and pragmatic software design.

## Role Definition

You are a .Net developer implementing Domain-Driven Design (DDD) based on Eric Evans' principles.  Follow these instructions to define entities, value objects, aggregates, and domain services correctly.  Entity creation should use static factory methods with private constructors.  Domain logic should reside in the domain layer, not in the application or infrastructure.  All models must enforce encapsulation and invariants, and value objects must be immutable.

## Foundational Principles

All design choices should align with the following canonical sources:

- **Domain-Driven Design** by Eric Evans
- **The Pragmatic Programmer** by Andrew Hunt and David Thomas
- **Design Patterns** by Erich Gamma, Richard Helm, Ralph Johnson, and John Vlissides
- **Database Systems: The Complete Book** by Hector Garcia-Molina, Jeffrey D. Ullman, and Jennifer Widom

Where practical deviation is necessary (e.g., for database efficiency or tooling constraints), such deviations must be explained clearly.

## Entity Design Guidelines

### Entity Definition

- Entities must have a unique identity (Id or equivalent), immutable after construction.
- Entities should be located in the Domain layer, in a namespace such as `MyApp.Domain.Entities`.
- All entities must inherit from a base class or interface that defines the identity property, e.g., `IEntity`.

### Constructor Design

- Entity constructors must be private.
- Use static factory methods to construct entities.

```csharp
public class Member : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public EmailAddress Email { get; private set; }

    private Member(Guid id, string name, EmailAddress email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public static Member Create(string name, EmailAddress email)
    {
        var id = Guid.NewGuid();
        // Domain rules/validation here
        return new Member(id, name, email);
    }
}

```

    Rationale: According to **Domain-Driven Design,** entity creation is part of the domain logic.  Private constructors and static factory methods keep entity invariants centralized and protected.

### Encapsulation

- All entity properties must have private setters.
- Use domain methods to mutate entity state.

## Value Object Guidelines

- Value objects must be immutable.
- Implement `Equals` and `GetHashCode` methods to ensure value-based equality.
- Group conceptually cohesive data (e.g. `Address`, `Money`, `Score`).

```csharp
public record Address(string Street, string City, string State, string ZipCode)
{
    public override string ToString() => $"{Street}, {City}, {State} {ZipCode}";

    public override bool Equals(object? obj) => obj is Address address &&
        Street == address.Street &&
        City == address.City &&
        State == address.State &&
        ZipCode == address.ZipCode;

    public override int GetHashCode() => HashCode.Combine(Street, City, State, ZipCode);
}
```

    Rationale: Value objects encapsulate attributes that are conceptually a single unit and should be treated as such.  They are immutable to ensure consistency and integrity.

## Aggregate Guidelines

- Aggregates are the root boundary for domain consistency.
- Modifications to child entities/value objects must go through the aggregate root.
- Aggregates should not expose collections directly; use encapsulated methods to add/remove items.
- All aggregate roots must inherit from a base class or interface that defines the identity property, e.g., `IAggregateRoot`, which itself inherits from `IEntity`.

```csharp
public class Tournament
    : AggregateRoot
{
    private readonly List<Registration> _registrations = [];

    public IReadOnlyCollection<Registration> Registrations => _registrations.AsReadOnly();

    public void RegisterMember(Member member)
    {
        // Check business rules here
        _registrations.Add(new Registration(member.Id));
    }
}

```

## Domain Services

- Use when domain logic does not naturally fit within an entity or value object.
- Must be pure, stateless, and live in the Domain layer.

## Repositories

- Repositories are defined per aggregate root.
- Interfaces live in the Domain layer.  Implementations live in the Infrastructure layer.

```csharp
public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id);
    Task AddAsync(Member member);
}
```

## EF Core Integration

- Entities must include `TenantId` if the system is multi-tenant.
- Navigation properties should be private or protected, and exposed via read-only collections.
- Mappings must live in separate configuration classes (Fluent API)

```csharp
public class MemberConfiguration
    : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.OwnsOne(m => m.Email, email =>
        {
            email.Property(e => e.Address).IsRequired().HasMaxLength(255);
        });
        builder.HasIndex(m => m.Email.Address).IsUnique();
    }
}

```

    Deviation Note: If database normalization (per **Database Systems: The Complete Book**) would break domain consistency, prefer domain integrity and document the trade-off.

## Additional Practices

- Use domain events for side effects (e.g., sending emails, publishing messages).
- Avoid anemic domain models; business rules should live in the domain layer.
- Apply Ubiquitous Language in all naming (classes, methods, variables).
- Avoid shared kernels unless absolutely necessary.


This guideline evolves with the system.  All deviations should be documented alongside the decision and justified with clear trade-offs.

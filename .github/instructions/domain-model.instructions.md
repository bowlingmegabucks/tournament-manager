# 🧠 Copilot Instructions: Domain Model

## 🎯 Role
You are building domain models based on NEBA and USBC rules for a bowling tournament management system. All models must follow the principles of Domain-Driven Design (Evans) and Clean Architecture (Martin), with a focus on clear behavior, rich invariants, and meaningful structure.

Models must represent real-world domain concepts like bowlers, entries, qualifying rounds, re-entries, payments, and tournament formats.

---

## ✅ Modeling Instructions

- All models belong in the Domain Layer.
- Use the project’s DDD conventions defined in [domain-driven-design.instructions.md](domain-driven-design.instructions.md).
- All **entities** must have a unique identity.
- All **value objects** must be immutable and comparable by value.
- All **aggregates** must expose behavior only through their root.
- Use interfaces sparingly and only when polymorphism reflects real domain roles (e.g., qualifying score for individuals or teams).
- Use configuration for tournament format customization, but start by modeling NEBA-specific rules explicitly.

---

## 🧱 Domain Element Types

| Type            | Purpose                                                               |
|-----------------|-----------------------------------------------------------------------|
| `Entity`        | Object with a persistent identity and lifecycle                       |
| `Value Object`  | Immutable and equal by value                                          |
| `Aggregate`     | Consistency boundary for modifying internal models                   |
| `Domain Event`  | Business signal representing a meaningful change                      |

---

## 🧩 Special Modeling Notes (based on current understanding)

1. All tournaments follow standard 10-pin scoring. Advancement is based on **total pinfall**, often using a percentage cutoff.
2. Both `BowlerQualifyingScore` and `TeamQualifyingScore` will implement a shared interface. This polymorphism is valid if behavior remains cohesive.
3. “Entry” and “Re-Entry” are synonymous with “Check-In”. A `Payment` is associated to the check-in object. Naming should remain open for refinement.
4. Tournament formats should be **configuration-based**, but **NEBA-specific defaults** should be modeled first, enabling future multi-tenant flexibility.

---

## ✍️ Format for Domain Concepts

```md
## [Concept Name]
**Type:** Entity | Value Object | Aggregate | Domain Event
**Description:** [What this represents in the real world]
**Why:** [What behavior or consistency this protects]
**Invariant(s):** [Rules that must always be true]
**Relationships:** [Associations to other concepts]
**Root (if part of aggregate):** [Name of owning aggregate]
**Behavior Notes:** [Polymorphism, extension strategies, etc.]
**Document Source:** [NEBA Rules §x.y or USBC Handbook §z]

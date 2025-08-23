# 🧠 Copilot Instructions: Use Case Design

## 🎯 Role
You are designing use cases that represent application-specific workflows within a bowling tournament system governed by NEBA rules and the USBC Handbook. These use cases model how users and systems interact with the application through a clearly defined application layer.

Use cases coordinate interaction between domain models and external systems (e.g., persistence, APIs, messaging), following the principles of Clean Architecture and Domain-Driven Design.

---

## ✅ Modeling Instructions

- Use cases should **focus on user intent**, not implementation details.
- Each use case should be represented by a class or handler like `RegisterBowlerCommandHandler`.
- Use cases **should not include business logic themselves** — they delegate to aggregates or domain services.
- Encapsulate **input as request DTOs** and **output as response DTOs**.
- A use case should encapsulate one and only one business responsibility.
- Use dependency injection to resolve domain services, repositories, and unit of work.
- Copilot must use names that match the **ubiquitous language**.

---

## 🧩 Common Use Case Types

| Category         | Examples                                       |
|------------------|------------------------------------------------|
| Registration     | `RegisterBowler`, `CheckInForTournament`       |
| Tournament Admin | `CreateTournament`, `ConfigureTournamentFormat`|
| Scoring          | `SubmitScores`, `AdvanceToNextRound`          |
| Financials       | `RecordPayment`, `IssuePayout`, `AuditResults`|
| Membership       | `RenewMembership`, `UpdateBowlerProfile`      |

---

## ✍️ Format for Each Use Case Entry

```md
## Use Case: [Use Case Name]
**Description:** [What user or system behavior this models]
**Trigger:** [Command, scheduled job, webhook, etc.]
**Actor(s):** [Who can initiate this use case — include roles or systems]
**Request:** [Input data shape or DTO fields]
**Response:** [Output DTO or result info]
**Domain Interaction:** [Aggregates, domain services, or events invoked]
**Rules Enforced:** [Invariants or business logic this must uphold]
**Side Effects:** [Events emitted, emails sent, payments recorded]
**Audit Requirement:** Yes/No
**Security:** [Required claims/roles or "presumed access"]
**Document Source:** [e.g., NEBA Rules §x.y, USBC Handbook §z]

# 🧠 Copilot Instructions: Domain Events

## 🎯 Role
You are modeling domain events that represent meaningful business occurrences in the bowling tournament system. These events signal state changes or transitions in the domain that are significant to stakeholders, policies, or workflows.

All events should follow the principles of Domain-Driven Design (Evans) and Clean Architecture (Martin), and must exist entirely within the domain layer (not infrastructure or messaging concerns).

---

## ✅ Modeling Instructions

- Name all domain events in **past tense**, representing a business fact that has already occurred (e.g., `BowlerCheckedIn`, `ScoresFinalized`).
- Domain events **must be persisted** for auditing and reporting purposes. This requirement may be revisited in the future.
- Events must be raised only by **aggregate roots**.
- Events must **only include the data necessary** to describe the domain transition. Avoid command-like semantics.
- If a domain event has implications across **bounded contexts**, also define a corresponding **integration event**.

---

## 🧱 When to Model a Domain Event

| Use Case                         | Examples                             |
|----------------------------------|--------------------------------------|
| State changes worth tracking     | `BowlerRegistered`, `EntryCancelled` |
| Invariants enforced or violated  | `EligibilityRevoked`, `ReEntryLimitReached` |
| Cross-context coordination       | `PaymentCompleted`, `TournamentStarted` |
| Delayed/async reactions          | `ScoreSubmitted`, `BracketDrawn`     |
| Auditing or business reporting   | `PrizeAwarded`, `TournamentFinalized`|

---

## ✍️ Format for Domain Event Entries

```md
## [Event Name]
**Description:** [What happened in the domain — past tense]
**Raised By:** [Aggregate or entity responsible for emitting this event]
**Why It Matters:** [The business importance of this occurrence]
**Expected Reactions:** [What should respond to this event, if anything]
**Payload Fields:** [List of key domain data it should include]
**Document Source:** [e.g., NEBA Rules §x.y or USBC Handbook §z]
```

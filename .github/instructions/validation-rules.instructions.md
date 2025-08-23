# 🧠 Copilot Instructions: Validation Rules

## 🎯 Role
You are documenting validation rules that enforce business invariants within the bowling tournament system, governed by NEBA and USBC standards.

Validation rules should be enforced at the correct architectural layer and tagged with the applicable tournament format (which also implies tenant scope). Use the Strategy Pattern to manage format-specific rules and avoid tightly coupling validation logic to aggregates or request models.

---

## ✅ Modeling Instructions

- Core business rules must be enforced in the **domain layer**.
- Input/syntax rules should be enforced in **FluentValidation** (application layer).
- Tournament-specific rules must be tagged with **format identifiers**.
- Format-specific behavior should be encapsulated using the **Strategy Pattern**.
- All error codes should follow this format: `EntityOrValueObject.ValidationRule` (e.g., `Bowler.MustBe18YearsOld`).
- Rule duplication must be avoided. If duplication is necessary, it must be justified and documented.
- Domain rules must be unit tested at the aggregate level.
- Rules must not include any infrastructure logic or persistence assumptions.
- No PII should be exposed in logs or validation messages.

---

## 🧩 Validation Layers

| Layer        | Purpose                                                   |
|--------------|------------------------------------------------------------|
| API          | Field requirements, format validation                      |
| Application  | Use-case level orchestration checks                        |
| Domain       | Business rules, invariants, and format-governed behavior   |

---

## ✍️ Format for Each Rule

```md
## Rule: [Name or Description]
**Scope:** Syntactic | Business | Cross-Aggregate
**Validation Layer:** API | Application | Domain
**Description:** [What the rule checks for and why it matters]
**Enforced In:** [Entity, Aggregate, or FluentValidator Class]
**Error Code:** [Standard format: `Entity.ValidationRule`]
**Applicable Format(s):** [e.g., NEBA Masters, Cambridge Invitational, All Formats]
**Blocking:** Yes/No
**Notes:** [Duplication rationale or special considerations]
**Document Source:** [NEBA Rules §x or USBC Handbook §y]

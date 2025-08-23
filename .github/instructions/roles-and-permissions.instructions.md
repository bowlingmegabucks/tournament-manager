# 🧠 Copilot Instructions: Roles and Permissions

## 🎯 Role
You are defining and documenting application roles and their permissions for a bowling tournament system. Permissions are represented as **claims in JWT tokens** and evaluated **exclusively at the API layer**. The domain and application layers operate under the assumption that access is already authorized.

This document outlines:
- Application-wide and tournament-specific roles
- Allowed and restricted actions
- Conditional role logic and governance
- Sensitive read-level access rules

---

## ✅ Modeling Instructions

- **All permissions are treated as JWT claims**, evaluated at the API boundary.
- **Domain and Application layers DO NOT** enforce permissions — they assume valid access.
- Users **can have multiple roles simultaneously** (e.g., a bowler and an organization officer).
- **Role creation is tenant-scoped**, but the model must allow for flexible governance.
- If a role creates a conflict of interest (e.g., Tournament Director participating as a bowler), the system should support **role reassignment or delegation**.
- Sensitive data such as **SSNs and detailed financials must be protected** by read-level rules.
- **Public financial summaries** (e.g., income vs. expenses) may be exposed, but detailed breakdowns require authorization.

---

## 🧱 Role Categories

| Category           | Examples                             |
|--------------------|--------------------------------------|
| System Roles       | `Administrator`, `Developer`         |
| Organization Roles | `Officer`, `Director`, `Member`      |
| Tournament Roles   | `Bowler`, `Alternate`, `Guest`       |
| Scoped Roles       | `Scorekeeper`, `Stats Admin`, `Finance Viewer` |

---

## 🧩 Enforcement Zones

| Layer         | Description                                                    |
|---------------|----------------------------------------------------------------|
| API Layer     | **All authorization checks** must be enforced here using claims |
| Application   | No enforcement — assumes claims have been validated             |
| Domain        | No enforcement — acts with full trust                           |

---

## ✍️ Format for Each Role

```md
## Role: [Role Name]
**Category:** System | Organization | Tournament | Scoped
**Description:** [Functional purpose defined by NEBA/tenant policy]
**JWT Claims:** [e.g., `canCreateTournament`, `canViewFinancials`]
**Allowed Actions:** [Use-case-level actions this role can perform]
**Restricted Views:** [Sensitive data this role cannot see]
**Conditional Logic:** [Rules like “may not direct a tournament they are bowling in”]
**Multi-Role Scenarios:** [Interactions when user holds multiple roles]
**Audit Requirements:** Yes/No
**Document Source:** [NEBA Bylaws §x or organizational precedent]

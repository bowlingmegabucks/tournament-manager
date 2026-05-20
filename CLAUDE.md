# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BowlingMegaBucks Tournament Manager is an internal web app being rewritten from a .NET/WinForms application to a React + Node.js/Express stack. The app manages bowling tournaments: setup, bowler registration, squad scheduling, score entry, handicap calculation, results/advancing/cashing, sweeper side events, payments, and IRS 1099 data.

**Hosted on GreenGeeks** (MariaDB + Node.js via cPanel). No WebSockets — the hosting environment cannot guarantee them.

## Spec Documents (read before generating code)

| Document                  | When to read                                                                          |
| ------------------------- | ------------------------------------------------------------------------------------- |
| `rewrite/REWRITE_SPEC.md` | Feature requirements, data model, all business rules                                  |
| `rewrite/PATTERNS.md`     | React + Express patterns and anti-patterns — read before any frontend or backend code |
| `rewrite/DATABASE.md`     | Authoritative schema with quirks; read before writing ORM models                      |
| `rewrite/THEMING.md`      | CSS design tokens, fonts, component visual patterns                                   |

## Project Structure

```
tournament-manager/
├── client/          # React app (Vite)
│   └── src/
│       ├── api/         # React Query hooks — one file per resource
│       ├── components/  # Shared/dumb UI components
│       ├── features/    # Feature modules (colocate component + hook + styles)
│       ├── pages/       # Route-level components (thin, delegate to features)
│       └── lib/         # Pure utilities (handicap calc, formatting)
├── server/          # Express API
│   └── src/
│       ├── routes/      # Express routers, one file per resource group
│       ├── controllers/ # Request/response handling (validate with Zod, call service)
│       ├── services/    # Business logic — no req/res objects
│       ├── middleware/  # Auth, error handler, rate limiter only
│       ├── db/          # ORM models and migrations
│       └── lib/         # Pure utilities (handicap calc mirrors client/src/lib)
├── rewrite/         # Spec and reference documents
└── legacy/          # Original .NET/WinForms app (reference only)
```

## Dev Commands

> Commands are TBD until the project is scaffolded. Update this section once `client/` and `server/` exist.

```bash
# Server
cd server && npm run dev

# Client
cd client && npm run dev

# Tests
npm test
```

## Tech Stack

- **Frontend:** React (Vite), React Router, React Query, shadcn/ui (TBD)
- **Backend:** Node.js LTS, Express.js, Sequelize or Prisma (MariaDB dialect)
- **Validation:** Zod — shared schemas between client and server where practical
- **Auth:** JWT (8h expiry); role claims: `director` | `staff` | `viewer`
- **PDF:** pdfkit or puppeteer (server-side)
- **Drag-and-drop:** @dnd-kit/core (lane assignments)

## Critical Business Rules

- **Scores stored as scratch.** Handicap is always computed at display/results time — never persisted.
- **`Math.floor()` everywhere** in ratio calculations (advancing, cashing, at-large). Never `round()` or `ceil()`.
- **Never block registrations** on eligibility. Show advisory warnings only; the TD decides. The submit button must never be disabled for eligibility reasons.
- **All advancing/cashing is per division.** Never aggregate across divisions.
- **SSNs encrypted at rest** (AES-256-GCM). Never log or expose in non-director responses. Only `GET /bowlers/:id/ssn` (director only) decrypts.
- **Confirmation codes** on payments must be unique across the system.
- A bowler can only advance to finals **once per division**, even if they bowl multiple squads.
- The squad-level `FinalsRatio` and `CashRatio` override tournament-level values when non-null.

## Key DB Schema Quirks

Read `rewrite/DATABASE.md` in full before writing ORM models. The most critical gotchas:

1. `Tournaments.SuperSweperCashRatio` — one 'e' in "Sweper" is intentional (typo in production schema). Do not rename without a migration.
2. `Squads` uses single-table inheritance: `SquadType = 0` (tournament) and `SquadType = 1` (sweeper). Columns are conditionally applicable.
3. Many `Bowlers` text fields are `NOT NULL` but store `''` for absent values — not `NULL`.
4. The completed flag is `Complete` on `Squads` and `Completed` on `Tournaments`.
5. `SquadRegistration.LaneAssignment` is a `varchar(3)` on the junction table — empty string means unassigned. No separate lane assignment table.
6. No cascade delete on `Registrations → Bowlers/Divisions` or `SquadRegistration`/`SquadScores`. Enforce deletion order in service code.

## API Conventions

Base URL: `/api/v1`

All errors return:

```json
{ "error": { "code": "SNAKE_CASE_CODE", "message": "...", "details": [...] } }
```

Async route handlers must be wrapped with `asyncHandler` (Express 4 does not forward unhandled rejections to error middleware).

Layer contract: **Routes** parse params → **Controllers** validate with Zod and call services → **Services** contain business logic with no req/res objects.

## Pull Request Reviews

When reviewing any PR, check whether the changes affect anything a new developer needs to know — setup, running the app, deployment, project structure. If so, update `README.md` as part of the review.

## Code Anti-Patterns

- No `useEffect` for derived state or data fetching — use React Query.
- No `useCallback`/`useMemo` without a measured performance reason.
- No custom hook that only wraps a single `useState`.
- No middleware for logic that belongs in a service.
- No raw SQL string interpolation — always parameterized queries.
- No `async` route handlers without `asyncHandler` wrapping.
- Pages are thin — no business logic in route-level components.

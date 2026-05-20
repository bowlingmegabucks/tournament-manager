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

```bash
# Server (port 3000)
cd server && npm run dev

# Client (port 5173, proxies /api → port 3000)
cd client && npm run dev

# Server tests
cd server && npm test

# Client tests
cd client && npm test
```

## Tech Stack

- **Frontend:** React (Vite), React Router, @tanstack/react-query, shadcn/ui, sonner (toasts)
- **Backend:** Node.js LTS, Express 5, Sequelize or Prisma (MariaDB dialect)
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

Routes use resource paths directly (e.g., `/tournaments`, `/bowlers`). No `/api` or version segment in the URL path — versioning is via the `x-api-version` request header, and in production the API lives on a dedicated domain that already signals "API".

All errors return:

```json
{ "error": { "code": "SNAKE_CASE_CODE", "message": "...", "details": [...] } }
```

Async route handlers must be wrapped with `asyncHandler` (Express 4 does not forward unhandled rejections to error middleware).

Layer contract: **Routes** parse params → **Controllers** validate with Zod and call services → **Services** contain business logic with no req/res objects.

## UI Messaging Strategy

Before introducing any new feedback pattern, ask a clarifying question — even if the answer seems obvious. The user drives product decisions; AI writes the implementation.

Use these rules consistently across all features:

| Scenario | Pattern | Component |
|---|---|---|
| Mutation success (save, update, delete) | **Toast — success** (auto-dismiss ~4 s) | `toast.success()` via sonner |
| Mutation failure (couldn't save/update) | **Toast — error** (longer, ~6 s) — user just triggered it and is already watching | `toast.error()` via sonner |
| Query failure (data didn't load) | **Inline `<ErrorBanner>`** in the section where the data should appear — with a Retry button | `client/src/components/ui/error-banner.tsx` |
| Form validation errors | **Inline under each field** — never block submit for business-rule reasons | React Hook Form + Zod |
| Destructive action confirmation | **Confirm dialog** before the action | shadcn `<AlertDialog>` |
| Auth failure (401) | **Redirect to `/login`** | React Router loader |
| Forbidden (403) | **Inline `<ErrorBanner>`** on the page — no redirect | `<ErrorBanner>` |
| System / network down | **Inline `<ErrorBanner>`** at the top of the affected section | `<ErrorBanner>` |
| Advisory-only eligibility warnings | **Inline callout below field** — never disable Submit | custom `<WarningCallout>` |

**Decision rules:**
- Toast = immediate feedback for actions the user *just triggered* (mutations). Never use a toast for a query that loads automatically.
- `ErrorBanner` = the content area is empty because data failed to load. It lives *where the data would be*, not globally at the top of the page.
- Do not combine both — pick one. If you're unsure which applies, ask.

## Pull Request Reviews

When reviewing any PR, check whether the changes affect anything a new developer needs to know — setup, running the app, deployment, project structure. If so, update `README.md` as part of the review.

## Testing Requirements

Every piece of code generated must have tests. **Always write tests as part of the same task — never defer them.**

### Server (Vitest)
- Every route, controller, and service gets unit tests.
- Route tests use `supertest` against the Express app.
- Target high mutation score: test boundary values, error branches, and edge cases — not just the happy path.
- Tests live next to the source file: `foo.ts` → `foo.test.ts`.

### Client (Vitest + React Testing Library)
- Every React component that renders conditional UI (loading states, error states, empty states) gets tests for each branch.
- Every `api/` hook gets tests with a mocked `fetch` — test success, error, and loading states.
- Tests live next to the source file.

### End-to-End (Playwright) — deferred
- Playwright is the E2E framework. Add it when the first real user-facing feature is complete.
- E2E tests are functional/smoke — they cover complete flows, not component internals.
- Do not write E2E tests for temporary or scaffolding code.
- When adding Playwright, ask clarifying questions about which flows to cover first.

## AI Collaboration Ground Rules

All implementation code in this project is written by AI (Claude). The user reviews, drives decisions, and tests. Keep this in mind:

- **Ask clarifying questions before implementing any new UI pattern, data model decision, or architectural choice.** Even if the answer seems obvious, confirm it — one question upfront is cheaper than a wrong implementation.
- When introducing a new pattern for the first time (error display, form layout, data fetching shape), document it in CLAUDE.md so future AI instances follow it consistently.
- Prefer explicit, readable code over clever abstractions — the AI that reads this file next may not share your context.
- When asked to implement something that conflicts with an existing rule in this file, flag the conflict and ask which rule wins before writing code.

## Code Anti-Patterns

- No `useEffect` for derived state or data fetching — use React Query.
- No `useCallback`/`useMemo` without a measured performance reason.
- No custom hook that only wraps a single `useState`.
- No middleware for logic that belongs in a service.
- No raw SQL string interpolation — always parameterized queries.
- No `async` route handlers without `asyncHandler` wrapping.
- Pages are thin — no business logic in route-level components.

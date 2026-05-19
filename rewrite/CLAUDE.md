# BowlingMegaBucks Tournament Manager — Claude Code Guide

> Read `rewrite/REWRITE_SPEC.md` for full feature requirements, data model, and business rules.
> Read `rewrite/THEMING.md` for all visual/CSS design tokens and component patterns.
> Read `rewrite/PATTERNS.md` before generating any React or Express code.
> Read `rewrite/DATABASE.md` for the authoritative schema.

---

## Project Structure (target)

```
tournament-manager/
├── client/          # React app (Vite)
│   ├── src/
│   │   ├── api/         # React Query hooks (one file per resource)
│   │   ├── components/  # Shared/dumb UI components
│   │   ├── features/    # Feature modules (tournaments, squads, scores, …)
│   │   ├── pages/       # Route-level components (thin — delegate to features)
│   │   └── lib/         # Pure utilities (handicap calc, formatting, etc.)
├── server/          # Express API
│   ├── src/
│   │   ├── routes/      # Express routers, one file per resource group
│   │   ├── controllers/ # Request/response handling
│   │   ├── services/    # Business logic (no req/res objects here)
│   │   ├── middleware/  # Auth, error handler, rate limiter — nothing else
│   │   ├── db/          # ORM models and migrations
│   │   └── lib/         # Pure utilities (handicap calc is shared — keep in sync)
└── rewrite/         # Specification and reference documents
```

---

## Tech Stack

- **Frontend:** React (Vite), React Router, React Query, shadcn/ui (TBD)
- **Backend:** Node.js LTS, Express.js, Sequelize or Prisma (MariaDB dialect)
- **Validation:** Zod (shared schemas between client and server where possible)
- **Auth:** JWT (8h expiry, role claims: `director` | `staff` | `viewer`)
- **PDF:** pdfkit or puppeteer (server-side)
- **Drag-and-drop:** @dnd-kit/core

---

## Running the App

> Commands TBD once project is scaffolded. Update this section.

```bash
# Server
cd server && npm run dev

# Client
cd client && npm run dev

# Tests
npm test
```

---

## Key Conventions

- **Never block registrations** on eligibility — show advisory warnings only (see REWRITE_SPEC §5.6).
- **Scores stored as scratch** — handicap is always computed at display/results time, never persisted.
- **SSNs encrypted at rest** (AES-256-GCM). Never log or expose in non-director API responses.
- **All advancing/cashing is per division** — never aggregate across divisions.
- **`floor()` everywhere** in ratio calculations — never `round()` or `ceil()`.
- Confirmation codes on payments must be unique across the system.

---

## What NOT to Generate

See `rewrite/PATTERNS.md` for the full anti-patterns list. Short version:

- No `useEffect` for derived state or event responses
- No custom hook that wraps only a single `useState`
- No `useCallback`/`useMemo` without a proven performance reason
- No middleware for logic that should be a service function call
- No raw SQL string interpolation — always parameterized queries
- No `async` route handlers without `asyncHandler` wrapping

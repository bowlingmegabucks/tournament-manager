# Plan: Tournaments Feature

> Covers the tournament list (home page) and tournament detail portal.
> Authentication is intentionally deferred — all endpoints are unprotected for now.
> When a legacy feature is migrated to the new app, delete the corresponding files from `legacy/`.

---

## Decisions Recorded

| Question | Answer |
|---|---|
| Home page layout | Hybrid — table on desktop (md+), cards on mobile |
| Navigate to tournament | `/tournaments/:id` (full page navigation) |
| Create tournament | Deferred — not in scope for this feature |
| Edit tournament | Inline on the detail page overview section |
| Detail page nav | Sidebar (Option B) with phase groupings |
| Squad detail | Future sub-portal (not in scope here) |
| Mobile responsive | Yes — all layouts |
| ORM | Prisma (MariaDB dialect) |
| Money over the wire | `number` (JS decimal, e.g. `50.00`) — format to locale string for display |
| Dates over the wire | ISO 8601 string (`"2025-10-10"`) — localize with `Intl.DateTimeFormat` for display |

---

## ORM: Prisma

Using **Prisma** with the MariaDB dialect. Schema lives in `server/prisma/schema.prisma`. Migrations use `prisma migrate` against the existing production schema — the first migration introspects the live DB.

---

## Routing Structure

React Router is not yet wired up in this app. The `main.tsx` renders `<App>` directly and App uses plain `<a href>` links. This feature introduces React Router.

```
/                               → redirect to /tournaments
/tournaments                    → TournamentsPage
/tournaments/:id                → redirect to /tournaments/:id/overview
/tournaments/:id/overview       → editable tournament details (built now)
/tournaments/:id/divisions      → placeholder "not yet built"
/tournaments/:id/squads         → placeholder "not yet built"
/tournaments/:id/registrations  → placeholder "not yet built"
/tournaments/:id/lanes          → placeholder "not yet built"
/tournaments/:id/results        → placeholder "not yet built"
/tournaments/:id/sweepers       → placeholder "not yet built"
```

The `/tournaments/:id/*` routes use React Router nested routes with `<Outlet>`. The sidebar renders navigation links for all sections; unbuilt sections render a generic "Coming soon" placeholder — the URL structure is established from the start so future features slot in without route changes.

---

## API

### Endpoints

#### `GET /tournaments`
Returns the list of all tournaments for the home page.

```json
[
  {
    "id": "uuid",
    "name": "Fall Classic 2025",
    "start": "2025-10-10",
    "end": "2025-10-12",
    "bowlingCenter": "Bowlero Tucson",
    "completed": false
  }
]
```

Sorted by `start` descending (most recent first). No pagination for now.

#### `GET /tournaments/:id`
Returns full tournament details plus summary counts for the sidebar badges.

```json
{
  "id": "uuid",
  "name": "Fall Classic 2025",
  "start": "2025-10-10",
  "end": "2025-10-12",
  "bowlingCenter": "Bowlero Tucson",
  "entryFee": "50.00",
  "games": 6,
  "finalsRatio": 7.0,
  "cashRatio": 5.0,
  "superSweeperCashRatio": 4.0,
  "completed": false,
  "_counts": {
    "divisions": 2,
    "squads": 3,
    "registrations": 48
  }
}
```

Returns `404` with `{ "error": { "code": "TOURNAMENT_NOT_FOUND", "message": "..." } }` if the id doesn't match.

#### `PUT /tournaments/:id`
Updates tournament details. Body is validated with Zod — all fields required (no partial updates for now).

Request body:
```json
{
  "name": "Fall Classic 2025",
  "start": "2025-10-10",
  "end": "2025-10-12",
  "bowlingCenter": "Bowlero Tucson",
  "entryFee": 50.00,
  "games": 6,
  "finalsRatio": 7.0,
  "cashRatio": 5.0,
  "superSweeperCashRatio": 4.0,
  "completed": false
}
```

Validation rules (mirrors legacy `TournamentControl` validation):
- `name` — non-empty string
- `start`, `end` — valid ISO date strings; `start` must be ≤ `end`
- `bowlingCenter` — non-empty string
- `entryFee` — positive number
- `games` — integer ≥ 1
- `finalsRatio` — number > 1
- `cashRatio` — number > 1
- `superSweeperCashRatio` — number > 1
- `completed` — boolean

Returns the updated tournament object on success. Returns `422` with field-level errors on validation failure.

---

### Server Files

```
server/src/
├── db/
│   └── models/
│       └── Tournament.ts          ← ORM model (Tournaments table)
├── routes/
│   └── tournaments.ts             ← Express router: mounts controller handlers
├── controllers/
│   └── tournaments.controller.ts  ← Zod validation, calls service, shapes response
└── services/
    └── tournaments.service.ts     ← DB queries, business logic, no req/res
```

Tests (next to source):
- `server/src/routes/tournaments.test.ts` — supertest integration tests for all 3 routes
- `server/src/controllers/tournaments.controller.test.ts` — unit tests for validation logic
- `server/src/services/tournaments.service.test.ts` — unit tests for service logic

Wire up: register `tournamentsRouter` in `server/src/app.ts`.

#### DB column mapping note
`Tournaments.SuperSweperCashRatio` has one "e" in "Sweper" — this is the production column name. The ORM model must map `superSweeperCashRatio` (JS) → `SuperSweperCashRatio` (DB). Never rename the DB column.

---

## Client

### File Structure

```
client/src/
├── pages/
│   ├── TournamentsPage.tsx         ← route component — renders <TournamentList />
│   └── TournamentDetailPage.tsx    ← route component — renders <TournamentDetailLayout />
├── features/
│   └── tournaments/
│       ├── TournamentList.tsx          ← hub: renders desktop or mobile based on breakpoint
│       ├── TournamentListDesktop.tsx   ← shadcn Table with columns: Name, Dates, Center, Status
│       ├── TournamentListMobile.tsx    ← list of <TournamentCard />
│       ├── TournamentCard.tsx          ← single mobile card
│       ├── TournamentDetailLayout.tsx  ← outer shell: sidebar + <Outlet />
│       ├── TournamentDetailSidebar.tsx ← sidebar nav with phase groupings
│       ├── TournamentDetailHeader.tsx  ← name, Active/Completed badge, meta strip
│       └── TournamentOverview.tsx      ← editable details panel (the built section)
└── api/
    └── tournaments.ts              ← useTournaments, useTournament, useUpdateTournament
```

### React Router Setup

Update `client/src/main.tsx` to wrap the app in `<BrowserRouter>` (or use `createBrowserRouter`).

Update `client/src/App.tsx` to define the route tree:

```tsx
<Routes>
  <Route index element={<Navigate to="/tournaments" replace />} />
  <Route path="/tournaments" element={<TournamentsPage />} />
  <Route path="/tournaments/:id" element={<TournamentDetailPage />}>
    <Route index element={<Navigate to="overview" replace />} />
    <Route path="overview" element={<TournamentOverview />} />
    <Route path="divisions" element={<ComingSoon section="Divisions" />} />
    <Route path="squads" element={<ComingSoon section="Squads" />} />
    <Route path="registrations" element={<ComingSoon section="Registrations" />} />
    <Route path="lanes" element={<ComingSoon section="Lane Assignments" />} />
    <Route path="results" element={<ComingSoon section="Results" />} />
    <Route path="sweepers" element={<ComingSoon section="Sweepers" />} />
  </Route>
</Routes>
```

The existing header nav `<a href>` links should become React Router `<Link>` components to avoid full-page reloads.

### Sidebar Nav — Sections and Grouping

```
Setup
  Overview        /tournaments/:id/overview
  Divisions       /tournaments/:id/divisions
  Squads          /tournaments/:id/squads

During Event
  Registrations   /tournaments/:id/registrations
  Lane Assignments /tournaments/:id/lanes

After Event
  Results         /tournaments/:id/results
  Sweepers        /tournaments/:id/sweepers
```

Active link is highlighted with a left border accent (matching theming). On mobile, the sidebar collapses to a horizontal scrollable strip at the top of the content area (the main content fills full width below it).

### Tournament List

**Desktop (md+ breakpoint):** shadcn `<Table>` with columns:
- Name (clickable → navigates to detail)
- Start date
- End date
- Bowling Center
- Status badge (`Active` green / `Completed` gray)

Sortable by name and start date would be nice but is not required in v1.

**Mobile:** Vertical list of `<TournamentCard>` components, each showing name, date range, bowling center, and status badge. Tap anywhere on the card to navigate.

Both views show an `<ErrorBanner>` in place of the list if the query fails, with a Retry button. Loading state uses a skeleton placeholder.

### Tournament Overview (edit panel)

The overview section shows all tournament fields as a read-only summary by default. An "Edit" button in the header (or inline) enters edit mode, replacing the summary with a form.

- Form uses React Hook Form + Zod schema validation
- Inline field-level errors (per CLAUDE.md messaging strategy)
- Save → `PUT /tournaments/:id` → success toast, exits edit mode
- Cancel → discards changes, exits edit mode (no confirmation needed since no destructive data loss)
- Error on save → error toast

Fields: Name, Start date, End date, Bowling Center, Entry fee, Games, Finals ratio, Cash ratio, Super Sweeper cash ratio, Completed checkbox.

### API Hooks (`client/src/api/tournaments.ts`)

```ts
// List
useTournaments(): UseQueryResult<TournamentSummary[]>

// Detail
useTournament(id: string): UseQueryResult<TournamentDetail>

// Update
useUpdateTournament(): UseMutationResult<TournamentDetail, Error, { id: string; data: TournamentUpdateInput }>
```

On successful `useUpdateTournament`, invalidate the `['tournament', id]` and `['tournaments']` query keys.

### Types

Define TypeScript types in `client/src/api/tournaments.ts` (co-located with hooks, not a separate types file). No shared package yet — duplication between client and server types is acceptable at this scale.

```ts
export type TournamentSummary = {
  id: string
  name: string
  start: string          // ISO date string "2025-10-10"
  end: string            // ISO date string "2025-10-12"
  bowlingCenter: string
  completed: boolean
}

export type TournamentDetail = TournamentSummary & {
  entryFee: number       // decimal, e.g. 50.00
  games: number
  finalsRatio: number
  cashRatio: number
  superSweeperCashRatio: number
  _counts: { divisions: number; squads: number; registrations: number }
}

export type TournamentUpdateInput = Omit<TournamentDetail, 'id' | '_counts'>
```

---

## Tests

### Server
- Route tests via supertest: list returns array, detail returns full object, update returns updated object, 404 on bad id, 422 on invalid body
- Service tests: list sorted correctly, counts accurate, update persists fields
- Controller tests: each validation rule triggers correct error

### Client
- `TournamentList` — renders desktop table on wide viewport, cards on narrow; error state shows ErrorBanner; loading state shows skeleton
- `TournamentCard` — renders name, dates, center, badge
- `TournamentDetailLayout` — sidebar renders all nav sections; active link is highlighted
- `TournamentOverview` — read-only view matches data; edit mode shows form; save calls mutation; cancel discards
- `tournaments.ts` hooks — mocked fetch: success, error, loading states for each hook

### End-to-End (Playwright)

#### Setup

Install and configure when the tournaments feature is implemented (first real user-facing feature per CLAUDE.md).

**Location:** `e2e/` at the repo root (covers the full stack, not just client).

**Config file:** `playwright.config.ts` at the repo root.

```ts
// playwright.config.ts (abbreviated)
export default defineConfig({
  testDir: './e2e',
  use: {
    baseURL: 'http://localhost:5173',
  },
  webServer: [
    {
      command: 'cd server && npm run dev',
      port: 3000,
      reuseExistingServer: !process.env.CI,
    },
    {
      command: 'cd client && npm run dev',
      port: 5173,
      reuseExistingServer: !process.env.CI,
    },
  ],
})
```

The `webServer` block starts both server and client automatically before the test run — no manual `npm run dev` needed. On CI both are always started fresh; locally existing dev servers are reused.

Add to root `package.json`:
```json
"scripts": {
  "test:e2e": "playwright test"
}
```

**Database for E2E:** Tests run against a real local database seeded with known fixture data. Seed a fixed set of tournaments before each test file using a `beforeAll` hook that calls the API directly (POST) or inserts via Prisma client. Tear down in `afterAll`. Do not mock the database in E2E tests.

#### Test Files

`e2e/tournaments.spec.ts`

**Flow 1 — Tournament list**
- Navigate to `/` → assert redirect lands on `/tournaments`
- Assert the page heading or title contains "Tournaments"
- Assert at least one tournament row/card is visible (seeded fixture)
- Assert the seeded tournament's name, date range, and bowling center are displayed
- Assert the Active/Completed badge is present
- On desktop viewport (1280×800): assert a table is visible, cards are not
- On mobile viewport (390×844): assert cards are visible, table is not

**Flow 2 — Navigate to tournament detail**
- From the list, click the seeded tournament's name/row
- Assert URL changes to `/tournaments/:id/overview`
- Assert the tournament name appears in the page header
- Assert the sidebar is visible with all section labels (Overview, Divisions, Squads, Registrations, Lane Assignments, Results, Sweepers)
- Assert "Overview" link is active (has active styling)
- Assert the meta strip shows the correct dates and bowling center

**Flow 3 — Sidebar navigation**
- On the detail page, click "Divisions" in the sidebar
- Assert URL changes to `/tournaments/:id/divisions`
- Assert a "coming soon" or placeholder message is shown (not an error)
- Assert "Divisions" link is now active, "Overview" is not
- Click browser back → assert URL returns to `/tournaments/:id/overview`

**Flow 4 — Edit tournament (happy path)**
- On the detail page overview, click the Edit button
- Assert the form fields appear pre-filled with the current tournament values
- Change the tournament name to a new unique value
- Click Save
- Assert success toast appears
- Assert the form exits edit mode
- Assert the updated name is now shown in the header and/or overview

**Flow 5 — Edit tournament (cancel)**
- Click Edit
- Change the tournament name to something else
- Click Cancel
- Assert the original name is still displayed (change was discarded)
- Assert no toast appears

**Flow 6 — Edit tournament (validation)**
- Click Edit
- Clear the tournament name field
- Click Save
- Assert an inline error message appears under the name field
- Assert no success toast appears
- Assert the form stays open

---

## Legacy Cleanup

When this feature is complete and verified, delete the following from `legacy/`:

```
legacy/source/main/Tournaments/Retrieve/RetrieveTournamentsForm.cs
legacy/source/main/Tournaments/Retrieve/RetrieveTournamentsForm.Designer.cs
legacy/source/main/Tournaments/Retrieve/RetrieveTournamentsForm.resx
legacy/source/main/Tournaments/Add/AddTournamentForm.cs
legacy/source/main/Tournaments/Add/AddTournamentForm.Designer.cs
legacy/source/main/Tournaments/Add/AddTournamentForm.resx
legacy/source/main/Tournaments/Portal/TournamentPortal.cs
legacy/source/main/Tournaments/Portal/TournamentPortal.Designer.cs
legacy/source/main/Tournaments/Portal/TournamentPortal.resx
legacy/source/main/Controls/TournamentControl.cs
legacy/source/main/Controls/TournamentControl.Designer.cs
legacy/source/main/Controls/TournamentControl.resx
legacy/source/presentation/Tournaments/TournamentViewModel.cs
legacy/source/tests/BowlingMegabucks.TournamentManager.IntegrationTests/Tournaments/GetTournamentTests.cs
legacy/source/tests/BowlingMegabucks.TournamentManager.IntegrationTests/Tournaments/GetTournamentsTests.cs
legacy/source/tests/BowlingMegabucks.TournamentManager.IntegrationTests/Tournaments/TournamentEntityFactory.cs
legacy/source/tests/BowlingMegabucks.TournamentManager.UnitTests/Tournaments/TournamentViewModelTests.cs
```

> Note: `TournamentsExtensions.cs` and any Seeding/Results forms are left in place until those features are migrated.

---

## Display Conventions

These apply consistently across all tournament UI — add to `client/src/lib/` as pure formatters (not hooks):

| Data type | Wire format | Display format |
|---|---|---|
| Money (`entryFee`, ratios) | `number` (50.00) | `$50.00` via `Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' })` |
| Dates (`start`, `end`) | ISO string `"2025-10-10"` | `Oct 10, 2025` via `Intl.DateTimeFormat('en-US', { month: 'short', day: 'numeric', year: 'numeric', timeZone: 'UTC' })` — `timeZone: 'UTC'` prevents off-by-one when the local timezone is behind UTC |
| Date ranges | two ISO strings | `Oct 10 – 12, 2025` (compact) or `Oct 10 – Nov 2, 2025` (cross-month) |

Add `formatCurrency(n: number): string` and `formatDate(iso: string): string` to `client/src/lib/format.ts`. Both get unit tests.

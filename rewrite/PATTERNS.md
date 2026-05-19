# React + Express Patterns & Anti-Patterns

> Reference this document before generating any frontend or backend code.
> These rules exist to prevent the most common AI generation mistakes in React/Node codebases.

---

## React

### State Management

**Rule: match the state tool to the data's nature.**

| Data type | Right tool |
|-----------|-----------|
| Server data (tournaments, bowlers, scores) | React Query (`useQuery` / `useMutation`) |
| Shared UI state (sidebar open, selected tab) | React Context or Zustand |
| Local component state (form inputs, toggle) | `useState` |
| Derived from existing state | Compute inline — no state at all |

**Never:**
- Put server data in `useState` + `useEffect` — that's what React Query replaces.
- Create a Context for something only one or two components need.
- Store computed values (e.g., handicap total) in state — compute them from the raw scores.

---

### `useEffect` — Use Sparingly

`useEffect` is for **synchronizing with external systems** (DOM APIs, third-party libraries, subscriptions). It is not a general-purpose "run code when something changes" hook.

**Wrong — use an event handler instead:**
```tsx
// Bad: fires after render, causes extra cycle
useEffect(() => {
  setFullName(`${first} ${last}`);
}, [first, last]);

// Good: compute inline
const fullName = `${first} ${last}`;
```

**Wrong — use React Query instead:**
```tsx
// Bad
const [squad, setSquad] = useState(null);
useEffect(() => {
  fetch(`/api/squads/${id}`).then(r => r.json()).then(setSquad);
}, [id]);

// Good
const { data: squad } = useQuery({ queryKey: ['squad', id], queryFn: () => fetchSquad(id) });
```

**Legitimate `useEffect` uses:**
- Syncing to a non-React component (e.g., a third-party chart library)
- `document.title` updates
- Setting up/tearing down event listeners on `window`/`document`
- Initializing a library that takes a DOM ref

---

### `useCallback` and `useMemo`

**Default: don't add them.** Add them only when you have measured a performance problem or when a referentially stable value is required (e.g., passing a callback as a prop to a component wrapped in `React.memo`, or a dependency of another hook that would otherwise loop).

```tsx
// Wrong: wrapping everything "just in case"
const handleClick = useCallback(() => doThing(id), [id]);         // unnecessary
const label = useMemo(() => `Squad ${number}`, [number]);         // unnecessary

// Right: plain functions and values unless there's a concrete reason
const handleClick = () => doThing(id);
const label = `Squad ${number}`;
```

---

### Custom Hooks

A custom hook is justified when it **encapsulates a non-trivial stateful behavior** that is reused across multiple components or is complex enough to warrant isolation.

**Not justified:**
```tsx
// This is just useState — don't wrap it
function useIsOpen(initial = false) {
  const [isOpen, setIsOpen] = useState(initial);
  return { isOpen, open: () => setIsOpen(true), close: () => setIsOpen(false) };
}
```

**Justified:**
- A React Query wrapper for a specific resource with shared config (e.g., `useSquadResults(squadId)`)
- A hook that combines multiple queries with derived logic (e.g., `useFinalsSeeding(tournamentId)`)
- A drag-and-drop state machine
- An `useHandicap(registration, division)` that applies the USBC formula from REWRITE_SPEC §5.1

---

### Component Design

- **Prefer composition over configuration.** A component that accepts `variant="primary|secondary"` is fine; a component with 15 boolean props is a red flag.
- **Pages are thin.** Route-level components (`/pages`) should mostly compose feature components and pass data — no business logic.
- **Colocate by feature, not by type.** Put `SquadResults.tsx`, `useSquadResults.ts`, and `squadResults.css` together in `features/squads/`, not scattered across `components/`, `hooks/`, `styles/`.
- **No prop drilling past two levels.** If you're passing the same prop through three components, either lift it to context or rethink the tree.

---

### Forms

- Use a form library (React Hook Form is recommended) — do not manage individual field `useState` values.
- Zod schemas validate on the client; the same schema should mirror server-side validation.
- Eligibility warnings in registration forms are **advisory only** — never disable the submit button based on eligibility (see REWRITE_SPEC §5.6).

---

### React Query Conventions

```tsx
// Query key convention: [resource, id?, subresource?]
['tournaments']
['tournament', id]
['squad', id, 'results']
['bowler', id]

// One file per resource in client/src/api/
// e.g., client/src/api/squads.ts exports useSquad, useSquadResults, useUpdateSquadScores
```

- Invalidate narrowly — invalidate `['squad', id, 'results']` not all `['squad']` after a score update.
- Mutations should `onSuccess` invalidate the relevant queries.
- Do not store query results in component state.

---

## Express / Node.js

### Async Error Handling

Every async route handler **must** be wrapped. Unhandled promise rejections in Express 4 do not reach the error middleware.

```ts
// Wrap all async handlers
import asyncHandler from 'express-async-handler'; // or a local equivalent

router.get('/squads/:id', asyncHandler(async (req, res) => {
  const squad = await squadService.getById(req.params.id);
  if (!squad) return res.status(404).json({ error: { code: 'NOT_FOUND', message: 'Squad not found' } });
  res.json(squad);
}));
```

Or use Express 5 which handles this natively (check Node version on GreenGeeks first).

---

### Middleware — Keep It Minimal

Middleware is for **cross-cutting concerns** that apply to many routes: authentication, role checks, rate limiting, request logging, error formatting.

**Do not use middleware for:**
- Logic specific to one route or resource
- Data transformation that belongs in a service
- Anything that reads `req.params` to do business logic (that's a controller's job)

```ts
// Wrong: middleware doing resource-specific work
router.use('/squads/:id/scores', asyncHandler(async (req, res, next) => {
  req.squad = await Squad.findByPk(req.params.id); // don't do this in middleware
  next();
}));

// Right: controller fetches what it needs
router.put('/squads/:id/scores', authenticate, requireRole('staff'), asyncHandler(scoreController.bulkUpdate));
```

---

### Layering: Routes → Controllers → Services

```
Route:       Parse params, call controller, nothing else
Controller:  Validate input (Zod), call service, format response
Service:     Business logic, database calls — no req/res objects
```

```ts
// Service: pure business logic
async function getSquadResults(squadId: string): Promise<SquadResults> { ... }

// Controller: HTTP boundary
const getResults = asyncHandler(async (req, res) => {
  const parsed = z.string().uuid().safeParse(req.params.id);
  if (!parsed.success) return res.status(400).json(formatError('INVALID_ID', ...));
  const results = await squadService.getSquadResults(parsed.data);
  res.json(results);
});
```

---

### Error Responses

All errors use the envelope from REWRITE_SPEC §7:

```ts
// lib/errors.ts
export function apiError(code: string, message: string, details?: unknown) {
  return { error: { code, message, details } };
}

// In controllers:
return res.status(404).json(apiError('NOT_FOUND', 'Squad not found'));
return res.status(409).json(apiError('DUPLICATE_REGISTRATION', 'Bowler already registered in this division'));
```

Register a single error-handling middleware at the app level — do not `try/catch` and format errors in every controller.

---

### Input Validation

- Validate at the controller layer using Zod before any service call.
- Never trust `req.params`, `req.query`, or `req.body` without parsing.
- Share Zod schemas between client and server in a `shared/` package where practical.

```ts
const CreateRegistrationSchema = z.object({
  bowlerId: z.string().uuid(),
  divisionId: z.string().uuid(),
  average: z.number().int().min(0).max(300),
  superSweeper: z.boolean().default(false),
});
```

---

### Database

- **No raw SQL string interpolation.** Use ORM parameterized queries or tagged template literals with proper escaping.
- Keep migrations small and forward-only.
- Business logic (handicap calculation, advancing/cashing) lives in services, not in ORM hooks or database triggers.
- See `rewrite/DATABASE.md` for the authoritative schema.

---

### Security Reminders

- SSN: encrypt with AES-256-GCM before insert; decrypt only in the `GET /bowlers/:id/ssn` endpoint (director only); log access.
- JWT: validate signature and expiry on every protected route; check role claim for role-gated routes.
- Rate limit the auth endpoint separately (stricter) from general API routes.
- Never log SSNs, passwords, or API keys.

---

## Shared Business Logic

These calculations must match REWRITE_SPEC exactly — do not improvise:

| Logic | Location | Spec ref |
|-------|----------|---------|
| Handicap per game | `lib/handicap.ts` (shared) | §5.1 |
| Advancing / cashing per squad | `server/src/services/results.ts` | §5.2 |
| At-large + finals seeding | `server/src/services/results.ts` | §5.3 |
| Sweeper flat-pin handicap | `server/src/services/sweeper.ts` | §5.4 |
| Super Sweeper eligibility | `server/src/services/sweeper.ts` | §5.5 |

All ratio calculations use `Math.floor()` — never `Math.round()` or `Math.ceil()`.

# Tournament Manager Functional Overview & Migration Preparation

> Purpose: This document captures the current desktop (WinForms + MVP) tournament management application's functional domains, workflows, and architectural patterns to guide an interim cleanup/refactor (still WinForms, still MVP) and a later migration to a web application (framework-agnostic). It balances breadth and moderate depth so each area can be cleanly re‑implemented with minimal archaeology.

> Interpretation & Change Control Notice: This document represents an interpreted understanding of the current codebase and inferred business behavior. It is suitable for architectural proposals, migration planning, and exploratory design discussions—not a final source of truth for contractual business rules. If in future conversations new or corrected business requirements are supplied that differ from content here, explicitly call out the discrepancy (Current Spec vs New Requirement), seek confirmation, and upon confirmation update this document accordingly (including revision date and a brief change summary). Until clarified, avoid implementing irreversible changes based solely on ambiguous or conflicting statements.

> Ubiquitous Language Commitment: As domain terminology is refined, this document must be updated to reflect the agreed Ubiquitous Language. If you (the SME) introduce a term I have not previously seen here, or you use an existing term with different semantics than currently documented, I will (a) explicitly highlight the mismatch, (b) ask clarifying questions until the meaning, boundaries, and invariants are unambiguous, and then (c) revise the Glossary, affected sections, and any model/command names to stay aligned. No new or altered domain term should be treated as canonical until explicitly confirmed. Expect pointed follow‑up questions (e.g., lifecycle states, uniqueness rules, scope of identity, temporal constraints) for every newly introduced or redefined term.

> Canonical Vocabulary Source: The authoritative Ubiquitous Language table is maintained in `README.md`. This specification mirrors those definitions for convenience; if discrepancies arise, the README prevails and this document must be updated to match.

---
## 1. Domain Scope (What the System Manages)
The system manages competitive bowling tournaments and related operational data:
- Tournaments: Containers for all other entities (divisions, squads, registrations, sweepers, lane assignments, scores, results).
- Divisions: Logical competitive groupings (scratch vs handicap, age, gender, etc.). Handicap settings drive validation (e.g., requiring USBC ID / average).
- Squads: Scheduled time slots or blocks in which registered bowlers compete.
- Bowlers: Participants (identity + eligibility attributes: name, gender, average, DOB, USBC ID, etc.).
- Registrations: The hub entity linking a bowler to a tournament division, squads, optional sweepers, and payments.
- Sweepers: Optional side competitions / prize pools (including a special “Super Sweeper”).
- Lane Assignments: Mapping of registrations (or squad positions) to physical lanes for a squad session.
- Scores: Game / series scores linked to lane assignments and registrations, feeding standings and reports.
- Reports / Results: PDF / print artifacts (seeding, at-large, squad results, sweeper results).
- Payments (modeled): Track financial aspects of registrations and add‑ons (not necessarily full payment gateway integration at this stage).

---
## 2. Layered Architecture (Current State)
```
+-------------------------------+
| Presentation (WinForms Views) |
|  - Forms implement IView*     |
+---------------+---------------+
                | MVP (Presenter orchestrates)
+---------------v---------------+
| Presentation Adapters /       |
| Application Orchestration     |
|  - Presenters                 |
|  - Adapters (retrieve/update) |
+---------------+---------------+
                | Commands / Queries
+---------------v---------------+
| Business Logic (Domain Core)  |
|  - Command Handlers           |
|  - Repositories               |
|  - Validators                 |
|  - Entity Mappers             |
|  - Strongly Typed IDs         |
+---------------+---------------+
                | Persistence (EF Core assumed)
+---------------v---------------+
| Data Context / Entities       |
+-------------------------------+
```
Supporting concerns:
- Telemetry (e.g., RegistrationTelemetry).
- Encryption utilities.
- Result reporting (QuestPDF based) reusable base class.

---
## 3. Cross-Cutting Patterns & Conventions
| Aspect | Current Approach | Migration Guidance |
|--------|------------------|--------------------|
| MVP Separation | WinForms Forms + Presenter classes + View interfaces | Preserve logical Presenter → map to Controllers / Service layer or React container hooks later. Keep view contracts small & intention-revealing. |
| Commands / Handlers | Append / Update / Delete style handlers for core flows | Formalize command & query boundaries; consider mediating layer (optional) to decouple UI entirely. |
| Validation | Use case validators (e.g., Create/UpdateRegistrationValidator) | Centralize invariants; extract to reusable validation library; prepare for client/server rule mirroring. |
| Strongly Typed IDs | Distinct types per aggregate (BowlerId, RegistrationId, etc.) | Maintain for type safety; translate to branded/opaque types in the web tier. |
| Repositories | Per aggregate access; sometimes mixed concerns | Move toward narrower repository or explicit persistence services; expose query DTO endpoints. |
| Telemetry | Ad‑hoc counters around registrations | Define unified logging/metrics contract early in interim refactor. |
| Reporting | QuestPDF + PrintDialog | Abstract “Report Generation” behind an interface; decouple from UI thread; plan export endpoints (PDF, CSV, JSON). |
| Error Handling | Presenter inspects adapter.Error strings | Normalize to a Result<T> or Problem contract early; map cleanly to HTTP problem details later. |
| Incremental UI Updates | Methods like UpdateBowlerName, RemoveRegistration | Keep granular mutation semantics; design differential state update endpoints for future real-time UI. |

---
## 4. Functional Areas (Moderate Depth)
### 4.1 Tournaments
- Create new tournament with metadata (dates, name, structure).
- Retrieve list (with filtering or simple load) for selection.
- Seeding workflow: Computes finals seeding (division filtered) and outputs via reusable report base.
- At-Large results: Aggregates bowlers outside auto-qualifiers.
- Reporting capabilities: PDF generation & print via `ResultReportBase<T>` (QuestPDF).
- Interim Refactor Goals:
  - Ensure tournament aggregate boundaries clear (divisions loaded lazily or explicitly).
  - Extract seeding computation logic into pure service for easy unit test coverage.

### 4.2 Divisions
- Define competition grouping with optional handicap parameters (e.g., percentage, base score, max average, etc. – inferred from validation patterns).
- Impact validation for registration (requiring USBC ID / average for handicap type divisions).
- Interim Goals: Centralize handicap rules into a domain service separate from UI or persistence.

### 4.3 Squads
- Represent specific time blocks bowlers compete in.
- Bowlers (via registrations) may select one or multiple squads.
- Results per squad use `SquadResultReport` to output standings.
- Interim Goals: Model capacity limits & fill percentages explicitly; prepare occupancy query endpoint.

### 4.4 Bowlers
- Store demographic + eligibility attributes (name, gender, DOB, average, USBC ID).
- Updated indirectly during registration update flows.
- Interim Goals: Normalize unique constraints (USBC ID uniqueness per tournament context if applicable). Add value objects (e.g., Average, UsbcNumber) for validation consolidation.

### 4.5 Registrations (Hub)
Core functions (already deeply analyzed):
- Create vs Append (merging additional selections for existing bowler in tournament).
- Update (demographics + division shift + squads/sweepers adjustments).
- Delete (with preconditions: ensure no locked downstream entities like posted scores—add explicit guard if not present).
- Add Super Sweeper (special side-event enrollment) idempotently.
- Provide counts for divisions, squads, sweepers for operations dashboard.
Interim Goals:
- Replace string-based error propagation with structured results.
- Enforce invariants (e.g., one registration per bowler per tournament) centrally.
- Introduce domain events for downstream recalculations (capacity, seeding refresh, etc.).

### 4.6 Sweepers
- Optional side competitions; each registration may enter selected sweepers and/or a global “Super Sweeper”.
- Result reports produced similarly to squads (aggregate scoring over games or series—details inferred).
- Portal-like presenter (tests reference `Sweepers.Portal.Presenter`) to load & complete a sweeper.
- Completion flow (marking payouts or finalization) via `CompleteAsync` pattern.
- Interim Goals: Explicit state machine (Draft → Open → Closed → Paid) to avoid ad-hoc booleans.

### 4.7 Lane Assignments
- Associate registrations (or squads) to physical lane identifiers (e.g., "15A", "7B").
- Consumed by Scores presenter to allow score entry per lane.
- Presenter logic retrieves lane assignments then binds to view; failures short-circuit subsequent score retrieval.
- Interim Goals: Extract lane allocation algorithm (balance, adjacency rules, avoiding repeats) if currently implicit.

### 4.8 Scores
- Entered per lane assignment + squad. Aggregated into standings (division / sweeper / at-large / seeding flows).
- Presenter pattern loads lane assignments first, then score sets.
- Interim Goals: Immutable score posting record (append-only) + derived aggregates rather than in-place mutation (improves auditability). Consider “locking” after cut line computation.

### 4.9 Results & Reporting
- Unified base (`ResultReportBase<T>`) provides: header composition (title, date, division), table skeleton, PDF generation, and print pipeline.
- Specific reports (Seeding, At-Large, Squad, Sweeper) implement column definitions + table population.
- Interim Goals: Introduce a report descriptor registry; allow offline generation (headless) for service-based export.

### 4.10 Payments
- Registration entity structure supports payments (line items / amounts) via mappers.
- Currently internal modeling; external processing not explicit.
- Interim Goals: Abstract payment line item generation (entry fee, sweeper fee, late fee, etc.) and reconcile totals; define integration seam (e.g., token/intent vs storing raw PII).

### 4.11 Validation
- Per-flow validators (Create/Update registration). Conditional requirements (USBC ID for handicap) enforced here.
- Interim Goals: Consolidate into domain invariant layer + expose rule metadata for dynamic UI form generation.

### 4.12 Telemetry & Logging
- Targeted instrumentation around registration lifecycle.
- Interim Goals: Adopt structured logging envelopes (CorrelationId, EntityId, Operation, Duration). Provide metric names stable for future dashboards.

### 4.13 Lane Assignments (Deep Dive)
Lane assignments are a critical, multi-user workflow with existing fragility that needs hardening prior to API + real-time enablement.

#### Current Interaction Model
The WinForms `LaneAssignments.Form` shows two lists implemented with `FlowLayoutPanel`:
1. Unassigned registrations (each a `LaneAssignmentControl` with bowler + division data, no lane code).
2. Lane placeholders (also `LaneAssignmentControl`) each with a `LaneAssignment` label (e.g., `22B`).

Drag-and-drop moves a bowler from unassigned → lane or reassigns an already placed bowler to a different lane. Context menu options allow removal. Division entry counts are tracked locally in a dictionary.

#### Key Implementation Details (Observed)
- `LaneAssignmentControl` implements `LaneAssignments.IViewModel` directly and also acts as a drag payload.
- Drag start: `(sender as Control)!.DoDragDrop((sender as IViewModel)!, DragDropEffects.Move);` (interface passed, but drop expects concrete control via `e.Data<LaneAssignmentControl>()`).
- `CompareTo` returns `0` unconditionally, making all instances logically equal (breaks equality semantics and may cause subtle issues in sets or dictionaries if introduced later).
- UI state is authoritative; there is no separation between view state and domain snapshot.
- Division counts are manually incremented/decremented (possible desync).
- Skip pattern radio buttons suggest lane allocation strategy (same vs staggered), but logic is not encapsulated in a domain service.

#### Identified Issues & Remedies
| Issue | Impact | Remedy |
|-------|--------|--------|
| Drag payload type mismatch (sending interface, expecting control) | Drop handlers can fail or produce null; risk of `NullReferenceException` | Standardize payload: pass control or DTO `LaneDragPayload`; update drop handlers accordingly. |
| `CompareTo` always returns 0 | All controls appear equal; equality/ordering broken | Implement comparison using `LaneAssignment` (lane code) + `BowlerId`; adjust `Equals` & operators. |
| UI = source of truth | Hard for multi-user sync; conflict-prone | Introduce a state store (projection) separate from controls; controls render from store. |
| Manual division counting | Drift risk after errors or missed events | Recalculate from projection OR maintain counts server-side and push via events. |
| Missing concurrency control | Last-write-wins overwrites silently | Add Version (row version) to lane assignment aggregate (even locally first). |
| Policy (capacity / skip) implicit | Hard to test & port | Create `ILaneAssignmentPolicy` domain service. |
| Lack of event model | No real-time updates | Emit domain events (AssignmentAdded, Moved, Removed) → later mapped to SignalR broadcasts. |

#### Target Domain Aggregate (API Future)
`SquadLaneSet` (identity: SquadId)
- Collection: `LaneSlot { LaneCode, BowlerId?, DivisionId, PositionIndex }`
- Invariants: one Bowler per Squad, capacity per pair, skip pattern enforced, lane code uniqueness.

#### Command / Query Set
Commands:
```
AssignLane { SquadId, LaneCode, BowlerId, ExpectedVersion }
MoveLaneAssignment { SquadId, FromLaneCode, ToLaneCode, ExpectedVersion }
UnassignLane { SquadId, LaneCode, ExpectedVersion }
BulkAutoAssign { SquadId, Strategy: { skipPattern, fillMode } }
```
Queries:
```
GetSquadLaneSnapshot { SquadId } -> { Version, Lanes[], Unassigned[] }
GetDivisionEntryCounts { SquadId } -> [{ DivisionName, Count }]
```

#### Real-Time Flow (Interim → Web)
1. Client sends command (drag intent) with ExpectedVersion.
2. Server processes, increments Version, persists aggregate.
3. Domain event -> SignalR Hub broadcast `LaneAssignmentChanged`.
4. Clients reconcile (apply if Version = currentVersion+1, else request snapshot).

#### Suggested SignalR Events
```
LaneAssignmentAssigned { squadId, laneCode, bowlerId, divisionId, version }
LaneAssignmentMoved { squadId, fromLaneCode, toLaneCode, bowlerId, version }
LaneAssignmentUnassigned { squadId, laneCode, bowlerId, version }
DivisionCountsUpdated { squadId, counts: [{ division, count }], version }
```

#### Drag & Drop Payload DTO (if not passing control)
```
LaneDragPayload {
   source: "unassigned" | "assigned",
   squadId: string,
   bowlerId: string,
   fromLaneCode?: string,
   divisionId: string,
   average: int,
   handicap: int
}
```

#### Metrics (Prep for Observability)
| Metric | Dimensions | Purpose |
|--------|------------|---------|
| lane_assignment_attempt_total | result, reason | Monitor failure causes (capacity, conflict, stale) |
| lane_assignment_latency_ms | operation | Performance and UX tuning |
| lane_assignment_version_gap_total | clientId | Detect sync issues requiring snapshot resend |

#### Incremental Refactor Steps (WinForms Interim)
1. Introduce `LaneAssignmentStateStore` (projection) decoupled from controls.
2. Fix drag payload; unify assign vs move in a single handler.
3. Implement proper `CompareTo` & equality.
4. Add Version to state store; include in presenter update calls.
5. Wrap lane operations behind an interface simulating future API (facade); presenters depend on interface.
6. Introduce local event dispatcher; fire events on state mutation.
7. Replace manual division counting with derived calculation from store.
8. Add policy service for skip & capacity logic + unit tests.
9. Prepare SignalR hub contract (even if stub) so method names & DTOs settle early.
10. Only after stability, wire actual API + SignalR, leaving presenter signatures intact.

### 4.14 Interim API Migration (Clean Architecture + DDD + CQRS)
Introduce the API layer now so both WinForms (interim) and Web (future) share the same contracts.

Layers:
```
Domain: Entities, Value Objects, Policies, Domain Events
Application: Command & Query Handlers, Validation, Event Publishing
Infrastructure: EF Core, Repositories, Outbox, SignalR Hubs
API: REST + SignalR endpoints (ASP.NET Core)
Clients: WinForms → Web UI
```

Principles:
- Domain free of transport/UI concerns.
- Commands idempotent when possible (assign same bowler to same lane = no-op success).
- Optimistic concurrency via Version/ETag; 409 Conflict returned on mismatch.
- Outbox pattern for reliable event publication to SignalR (and future integrations).
- Read models tailored for UI; no leaking internal entity invariants.

Example HTTP Endpoints:
```
POST /squads/{squadId}/lane-assignments/assign
POST /squads/{squadId}/lane-assignments/move
POST /squads/{squadId}/lane-assignments/unassign
POST /squads/{squadId}/lane-assignments/auto
GET  /squads/{squadId}/lane-assignments
GET  /squads/{squadId}/lane-assignments/division-counts
```

WinForms Consumption Strategy:
| Aspect | Approach |
|--------|----------|
| Snapshot Load | Initial `GetSquadLaneSnapshot` on form open |
| Real-Time | SignalR subscription updates state store |
| Conflict Handling | Retry on 409 by refreshing snapshot (optimistic concurrency) |
| Offline / Gap Recovery | Detect version gap >1 → auto snapshot refresh |
| Error Surface | Map problem details to friendly dialog messages |

Payoff: Web client can reuse identical command/query shapes; only view rendering changes.

---
## 5. Workflow Summaries (Narrative)
| Workflow | Trigger | Core Steps | Outputs | Key Risks |
|----------|---------|-----------|---------|----------|
| Register Bowler | User opens Add Registration | Load divisions → enter bowler data → validate → create/append → refresh counts | New registration, updated counts | Duplicate detection, invalid handicap data |
| Update Registration | Select registration → edit → save | Fetch current → validate → persist selective fields | Updated row | Division change side-effects |
| Delete Registration | Select → delete confirmation | Check dependencies → remove → UI row removal | Reduced counts | Orphaned scores if not guarded |
| Add Super Sweeper | Button/action on registration row | Check not already enrolled → persist flag | Row mutated (flag true) | Duplicate enrollment |
| Lane Assignment | Squad management UI | Determine open lanes → assign registrations → persist | Assignment grid | Over-capacity, conflicting lanes |
| Enter Scores | Scores form → select squad | Load lane assignments → load scores → edit/post | Updated standings | Partial posting, concurrency |
| Generate Report | Operator selects report | Build view models → instantiate report subclass → PDF/Print | Artifact File / Print job | Large data render performance |
| Complete Sweeper | Portal finalize action | Validate all scores posted → mark closed | Sweeper status updated | Premature closure |

---
## 6. Interim Refactor Recommendations (Pre-Web Step)
1. Formalize Application Layer:
   - Introduce a thin command/query facade (e.g., interfaces `IRegistrationService`, `ITournamentService`).
   - Presenters depend on these services instead of repositories directly.
2. Standardize Result Model:
   - Adopt `Result<T>` or `(Success, Value, Errors[])` pattern; remove stringly `Error` semantics.
3. Centralize Validation:
   - Domain rule services (e.g., `IHandicapRulesEvaluator`). Presenters only marshal data.
4. Reporting Abstraction:
   - Introduce `IReportGenerator` with pluggable implementations (QuestPDF now, service API later).
5. Side-Effect Isolation:
   - Emit domain events (e.g., `RegistrationCreated`, `ScoresPosted`) inside command handlers; presenters subscribe via adapters short-term.
6. Immutable Auditing:
   - Consider append-only score entries with derived aggregates.
7. Configuration & Secrets:
   - Externalize constants (fees, cap limits) to config/JSON; prepare for remote admin UI.
8. Typed IDs Everywhere:
   - Ensure no leakage of raw GUID/ints across layer boundaries (eases serialization consistency later).
9. Concurrency Protections:
   - Add optimistic concurrency tokens to critical aggregates (Registration, Scores) to prevent overwrite.
10. Telemetry Envelope:
    - Standardize logging extension methods (Begin/Success/Failure) for each command type.
11. Lane Assignments Hardening:
   - See section 4.13. Implement state store, fix drag payload, add versioning & events, encapsulate policies.

---
## 7. Migration-Oriented Model Contracts (Conceptual)
(Framework-agnostic sketches—NOT tied to a specific web stack.)

### 7.1 Registration (Read Model)
```
RegistrationSummary {
  registrationId: string
  bowler: { id: string, name: string, gender?: string, average?: number, usbcId?: string }
  division: { id: string, name: string, isHandicap: boolean }
  squads: [{ id: string, label: string, start: datetime }]
  sweepers: [{ id: string, name: string, super?: boolean }]
  payments: [{ code: string, amount: decimal }]
  flags: { superSweeper: boolean }
}
```

### 7.2 Command Examples (Future Service Layer)
```
CreateRegistrationCommand { tournamentId, bowlerInfo, divisionId, squadIds[], sweeperIds[], paymentSelections[] }
AppendRegistrationCommand { tournamentId, usbcId | bowlerId, squadIds[], sweeperIds[] }
UpdateRegistrationCommand { registrationId, bowlerPatch, divisionId?, squadIds?, sweeperIds? }
DeleteRegistrationCommand { registrationId, reason? }
AddSuperSweeperCommand { registrationId }
PostScoresCommand { squadId, laneAssignments: [{ lane, gameNumber, registrationId, score }] }
```

---
## 8. Reporting Abstraction Plan
| Current | Target Abstraction | Future Web Adaptation |
|---------|--------------------|-----------------------|
| Direct QuestPDF calls in forms | `IReportDefinition` + `IReportRenderer` | Server-side generation endpoint returning file stream; client triggers download or print preview |
| Synchronous UI blocking | Async background generation with progress | Notification/Job pattern (poll or push) |
| Static table composition per subclass | Column + Row descriptor metadata | Dynamic table components in web UI |

---
## 9. Risk & Gap Inventory
| Area | Risk | Mitigation in Interim |
|------|------|----------------------|
| Duplicate logic across presenters & handlers | Divergent behavior | Central services + tests |
| Implicit business rules (handicap, capacity) | Hard to port | Extract rule engines early |
| Reporting tightly coupled to WinForms | Rewrite effort | Introduce abstraction now |
| Error handling string-based | Fragile integration later | Structured error contracts |
| Missing concurrency checks | Data races in multi-user web | Add concurrency tokens now |
| Payment modeling incomplete | Rework at migration | Define line item taxonomy + totals early |
| No domain events | Spaghetti dependencies | Introduce lightweight event dispatcher |

---
## 10. Test Strategy Evolution
| Phase | Focus |
|-------|-------|
| Current | Unit tests around repositories, presenters (mock heavy) |
| Interim | Service-level tests (command handlers), invariant tests, report generation snapshot tests |
| Pre-Web | Contract tests for read models & command behaviors |
| Web Migration | API endpoint tests + end-to-end tournament lifecycle scenarios |

---
## 11. Suggested Refactor Order (Incremental Safety)
1. Introduce Result pattern + service facades (no UI change exposed).
2. Migrate existing presenters to call facades.
3. Extract validation + rule services; increase unit test coverage.
4. Add domain events + in-memory dispatcher; hook telemetry.
5. Abstract report generation.
6. Introduce concurrency tokens & auditing for registrations/scores.
7. Prepare serialization DTOs (read models + commands) matching section 7.
8. Document REST-ish endpoint design (internal doc) and map each presenter action.
9. Introduce API layer (Clean Architecture + DDD + CQRS) consumed by WinForms (dogfood future contract) plus SignalR hubs for lane assignments & registrations.
10. Replace Forms incrementally with thinner shells delegating to API (strangler pattern) until full web UI swap feasible.

---
## 12. Non-Functional Considerations
| Concern | Notes |
|---------|-------|
| Performance | Pre-compute standings/seeding if expensive (cache per tournament snapshot). |
| Scalability | Stateless command handlers make horizontal scaling easier later. |
| Security | Prepare for auth: role-based (Operator, Scorekeeper, Admin); sensitive fields (DOB, partial IDs) minimization. |
| Observability | Correlate operations by TournamentId + RegistrationId; standard log fields. |
| Internationalization | Date/time formatting centralized (currently CultureInfo aware in reports). |
| Accessibility | Future UI: ensure form semantics; keyboard navigation parity with WinForms power-user flows. |

---
## 13. Glossary
| Term | Definition |
|------|------------|
| Registration | The participation record of a bowler in a tournament division/squads/sweepers. |
| Sweeper | Optional side competition with its own prize distribution. |
| Super Sweeper | Premium/global sweeper enrollment flag. |
| Squad | Scheduled session time a set of bowlers compete. |
| Lane Assignment | Mapping of a registration (or slot) to a physical lane identifier. |
| Seeding | Ranking process to determine finals placement. |
| At-Large | Qualifiers outside automatic division-based advancement. |

---
## 14. Migration Readiness Checklist (To Maintain)
- [ ] All core flows behind service interfaces.
- [ ] Validation rules centralized & documented.
- [ ] Structured Result / Error contract adopted.
- [ ] Report abstraction implemented.
- [ ] Domain events emitted for key lifecycle changes.
- [ ] Registration & Scores concurrency tokens added.
- [ ] DTO layer defined & versioned.
- [ ] Tests: 80%+ coverage of command/services critical paths.
- [ ] Logging & metrics naming stable.
- [ ] Configuration externalized (fees, capacities, handicap parameters).

---
## 15. Next Steps
If approved, proceed to:
1. Add a lightweight `Application` project (services, results, events).
2. Wrap existing registration handlers inside facade and retrofit presenters.
3. Define domain event contracts & minimal dispatcher.
4. Draft endpoint contract doc (mirroring section 7) for future service/API alignment.

> This document should be refined as deeper dives uncover additional invariants (lane assignment rules, scoring aggregation specifics, sweeper payout logic). Treat it as a living artifact.

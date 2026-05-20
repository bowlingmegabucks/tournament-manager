# Bowling Tournament Manager — React/Node.js Rewrite Specification

> **Purpose:** This document is the authoritative guide for rebuilding the BowlingMegaBucks Tournament Manager as a React + Node.js/Express web application. It captures all business rules, data models, feature requirements, and integration points derived from the existing .NET/WinForms application.

---

## Table of Contents

1. [System Overview](#1-system-overview)
2. [Tech Stack](#2-tech-stack)
3. [User Roles & Permissions](#3-user-roles--permissions)
4. [Data Model](#4-data-model)
5. [Business Rules](#5-business-rules)
6. [Feature Modules](#6-feature-modules)
7. [API Specification](#7-api-specification)
8. [Third-Party Integration](#8-third-party-integration)
9. [Hosting & Deployment](#9-hosting--deployment)
10. [Security Requirements](#10-security-requirements)
11. [MVP Scope](#11-mvp-scope)

---

## 1. System Overview

The BowlingMegaBucks Tournament Manager is an internal web application used to manage bowling tournaments from setup through results. It handles:

- Tournament and division configuration
- Bowler profiles and registration
- Squad scheduling and lane assignments
- Score entry and handicap calculation
- Finals seeding (who advances, who cashes)
- Sweeper side events and the Super Sweeper prize pool
- Payment record-keeping
- IRS 1099 data collection

The system has two access modes:

- **Pre-tournament:** A third-party registration system pushes bowler registrations into the API.
- **During/after tournament:** Desk staff manage all registration, scoring, and results via the web UI.

---

## 2. Tech Stack

### Frontend

- **Framework:** React (latest stable)
- **Routing:** React Router
- **State Management:** TBD (React Query recommended for server state; Context or Zustand for UI state)
- **UI:** Component library TBD (e.g., shadcn/ui, Mantine, or MUI)
- **Drag-and-drop:** `@dnd-kit/core` or `react-beautiful-dnd` (required for lane assignments)
- **PDF generation:** Client-side via `react-pdf` or server-side via a Node.js PDF library (e.g., `pdfkit`, `puppeteer`)

### Backend

- **Runtime:** Node.js (LTS)
- **Framework:** Express.js
- **ORM:** Sequelize or Prisma (MariaDB dialect)
- **Validation:** Zod or Joi
- **Authentication:** JWT (JSON Web Tokens) with role claims
- **Logging:** Winston or Pino
- **PDF:** pdfkit or puppeteer (server-side PDF generation)

### Database

- **Engine:** MariaDB (existing GreenGeeks-hosted instance)
- The existing database schema can be migrated to the new ORM or retained as-is with raw queries initially.

### Hosting

- **Platform:** GreenGeeks (EcoSite Premium or Managed VPS — Node.js support via cPanel)
- Node.js apps are configured in cPanel under **Software → Node.js**
- GreenGeeks provides limited Node.js support; deployment and debugging are the developer's responsibility
- See: https://www.greengeeks.com/support/article/create-nodejs-app-cpanel/

---

## 3. User Roles & Permissions

### Roles

| Role       | Description                                                                                                   |
| ---------- | ------------------------------------------------------------------------------------------------------------- |
| `director` | Tournament director. Full access to all features: create/edit tournaments, manage all data.                   |
| `staff`    | Desk staff. Can register bowlers, enter scores, manage squads. Cannot create/delete tournaments or divisions. |
| `viewer`   | Read-only access to scores and results. No edits.                                                             |

### Permission Matrix

| Feature                           | director              | staff | viewer |
| --------------------------------- | --------------------- | ----- | ------ |
| Create / edit / delete tournament | ✅                    | ❌    | ❌     |
| Create / edit / delete divisions  | ✅                    | ❌    | ❌     |
| Create / edit / delete squads     | ✅                    | ✅    | ❌     |
| Register bowlers                  | ✅                    | ✅    | ❌     |
| Edit bowler profiles              | ✅                    | ✅    | ❌     |
| Assign lanes                      | ✅                    | ✅    | ❌     |
| Enter / edit scores               | ✅                    | ✅    | ❌     |
| View scores and results           | ✅                    | ✅    | ✅     |
| Record payments                   | ✅                    | ✅    | ❌     |
| View SSN data                     | ✅                    | ❌    | ❌     |
| Receive API key (3rd party push)  | N/A — service account |       |        |

### Authentication

- Username + password login, returning a JWT
- JWT includes `userId`, `role`, and `exp`
- A separate **service account** (API key or JWT) is used by the third-party registration system — it has `staff`-level write access to registrations only
- The existing `x-api-key` scheme from the .NET API can be retained for the third-party integration endpoint

---

## 4. Data Model

### 4.1 Bowler

Represents an individual bowler in the system. Bowlers are global (not per-tournament).

| Field                  | Type                  | Notes                                                                   |
| ---------------------- | --------------------- | ----------------------------------------------------------------------- |
| `id`                   | UUID / auto-increment | Primary key                                                             |
| `firstName`            | string                | Required                                                                |
| `middleInitial`        | string(1)             | Optional                                                                |
| `lastName`             | string                | Required                                                                |
| `suffix`               | string                | Optional (Jr., Sr., III, etc.)                                          |
| `street`               | string                | Optional                                                                |
| `city`                 | string                | Optional                                                                |
| `state`                | string(2)             | Optional, US state abbreviation                                         |
| `zip`                  | string                | Optional                                                                |
| `email`                | string                | Optional                                                                |
| `phone`                | string                | Optional                                                                |
| `usbcId`               | string                | USBC membership ID. Optional but important for sanctioned events        |
| `dateOfBirth`          | date                  | Required for age-restricted divisions                                   |
| `gender`               | enum(`M`, `F`)        | Required                                                                |
| `socialSecurityNumber` | string                | **Encrypted at rest.** Required for IRS 1099 reporting. See Section 10. |

**Notes:**

- A bowler can be registered in multiple tournaments and multiple divisions within the same tournament.
- Bowler identity is global — the same bowler record is reused across events.
- Display name = `firstName [middleInitial.] lastName [suffix]`

---

### 4.2 Tournament

Represents one bowling tournament event.

| Field                   | Type                  | Notes                                                                          |
| ----------------------- | --------------------- | ------------------------------------------------------------------------------ |
| `id`                    | UUID / auto-increment |                                                                                |
| `name`                  | string                | Required                                                                       |
| `startDate`             | date                  | First date of the event                                                        |
| `endDate`               | date                  | Last date (can equal startDate for one-day events)                             |
| `entryFee`              | decimal(10,2)         | Standard entry fee per registration                                            |
| `games`                 | integer               | Number of games bowled per squad                                               |
| `finalsRatio`           | integer               | 1-in-N entries advance to finals (e.g., 7 → 1 in 7)                            |
| `cashRatio`             | integer               | 1-in-N entries cash (e.g., 5 → 1 in 5)                                         |
| `bowlingCenter`         | string                | Name of the venue                                                              |
| `superSweeperCashRatio` | integer               | 1-in-N entries cash in the Super Sweeper prize pool. Null if no Super Sweeper. |
| `completed`             | boolean               | Set to true when the tournament is fully concluded                             |

---

### 4.3 Division

A division defines a competitive bracket within a tournament. Tournaments can have multiple divisions (e.g., Men's Open, Women's, Senior).

| Field                    | Type                  | Notes                                                                    |
| ------------------------ | --------------------- | ------------------------------------------------------------------------ |
| `id`                     | UUID / auto-increment |                                                                          |
| `tournamentId`           | FK → Tournament       |                                                                          |
| `name`                   | string                | e.g., "Men's Open", "Women's", "Senior Men's"                            |
| `number`                 | integer               | Display ordering within the tournament                                   |
| `minimumAge`             | integer               | Optional. Bowler must be at least this age on the tournament start date. |
| `maximumAge`             | integer               | Optional.                                                                |
| `minimumAverage`         | integer               | Optional. Bowler's declared average must be at or above this.            |
| `maximumAverage`         | integer               | Optional.                                                                |
| `handicapPercentage`     | decimal               | e.g., 0.90 for 90%. Zero for scratch.                                    |
| `handicapBase`           | integer               | e.g., 220. The base score handicap is calculated from.                   |
| `maximumHandicapPerGame` | integer               | Cap on handicap pins per game.                                           |
| `gender`                 | enum(`M`, `F`, `Any`) | Restricts the division to a gender, or allows any.                       |

---

### 4.4 Registration

A registration links a specific bowler to a specific division in a tournament.

| Field          | Type                  | Notes                                                                                                      |
| -------------- | --------------------- | ---------------------------------------------------------------------------------------------------------- |
| `id`           | UUID / auto-increment |                                                                                                            |
| `bowlerId`     | FK → Bowler           |                                                                                                            |
| `divisionId`   | FK → Division         |                                                                                                            |
| `average`      | integer               | The bowler's declared average for this registration. Used for handicap calculation and eligibility checks. |
| `superSweeper` | boolean               | True if the bowler has opted into the Super Sweeper (must bowl all defined sweepers)                       |

**Constraints:**

- Unique on `(bowlerId, divisionId)` — a bowler can only be registered once per division.
- A bowler can have multiple registrations in the same tournament if they are in different divisions.

---

### 4.5 Squad

A squad is a scheduled bowling session within a tournament. There are two types.

#### Common Fields

| Field           | Type                          | Notes                                                                                                              |
| --------------- | ----------------------------- | ------------------------------------------------------------------------------------------------------------------ |
| `id`            | UUID / auto-increment         |                                                                                                                    |
| `tournamentId`  | FK → Tournament               |                                                                                                                    |
| `type`          | enum(`tournament`, `sweeper`) | Discriminator                                                                                                      |
| `date`          | datetime                      | Date and time the squad bowls                                                                                      |
| `maxPerPair`    | integer                       | Maximum number of bowlers assigned to each lane pair                                                               |
| `startingLane`  | integer                       | The first lane number in use                                                                                       |
| `numberOfLanes` | integer                       | Total number of lanes in use                                                                                       |
| `finalsRatio`   | integer                       | Optional. Overrides the tournament-level `finalsRatio` for this squad only. If null, use tournament `finalsRatio`. |
| `cashRatio`     | integer                       | Optional. Overrides the tournament-level `cashRatio` for this squad only. If null, use tournament `cashRatio`.     |
| `completed`     | boolean                       | True once all scores have been entered and verified                                                                |

#### Tournament Squad (type = `tournament`)

| Field      | Type          | Notes                                                                                                                                                                    |
| ---------- | ------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `entryFee` | decimal(10,2) | Optional. Overrides the tournament-level `entryFee` for this squad only (e.g., an early-morning squad at a discounted rate). If null, the tournament `entryFee` applies. |

#### Sweeper Squad (type = `sweeper`)

| Field      | Type          | Notes                                                                     |
| ---------- | ------------- | ------------------------------------------------------------------------- |
| `entryFee` | decimal(10,2) | Entry fee for the sweeper (separate from tournament entry fee)            |
| `games`    | integer       | Number of games bowled in this sweeper (may differ from tournament games) |

**Notes:**

- A tournament can have multiple squads on different dates/times.
- A bowler can bowl multiple squads in the same tournament but can only advance to finals once (see Section 5.3).

---

### 4.6 SquadRegistration

Links a registration to a specific squad, indicating that bowler will bowl that session.

| Field            | Type              | Notes |
| ---------------- | ----------------- | ----- |
| `registrationId` | FK → Registration |       |
| `squadId`        | FK → Squad        |       |

Primary key is `(registrationId, squadId)`.

---

### 4.7 SquadScore

Stores an individual game score for a bowler in a squad.

| Field      | Type        | Notes                    |
| ---------- | ----------- | ------------------------ |
| `bowlerId` | FK → Bowler |                          |
| `squadId`  | FK → Squad  |                          |
| `game`     | integer     | Game number (1-based)    |
| `score`    | integer     | Raw (scratch) game score |

Primary key is `(bowlerId, squadId, game)`.

**Notes:**

- Scores are stored as scratch (raw pins). Handicap is calculated at display/results time.
- One row per game per bowler per squad.

---

### 4.8 Payment

Records a payment made for a registration.

| Field              | Type                  | Notes                            |
| ------------------ | --------------------- | -------------------------------- |
| `id`               | UUID / auto-increment |                                  |
| `registrationId`   | FK → Registration     |                                  |
| `amount`           | decimal(10,2)         | Amount paid                      |
| `confirmationCode` | string                | Unique confirmation/receipt code |
| `createdAt`        | datetime (UTC)        | Timestamp of payment record      |

---

### 4.9 SweeperDivision

Links sweeper squads to specific divisions, enabling gender- or bracket-specific sweeper results.

| Field            | Type                      | Notes |
| ---------------- | ------------------------- | ----- |
| `sweeperSquadId` | FK → Squad (type=sweeper) |       |
| `divisionId`     | FK → Division             |       |

---

## 5. Business Rules

### 5.1 Handicap Calculation

**Formula (USBC standard):**

```
handicapPerGame = floor((handicapBase - bowlerAverage) × handicapPercentage)
handicapPerGame = max(handicapPerGame, 0)        // can't go negative
handicapPerGame = min(handicapPerGame, maxHandicapPerGame)  // apply cap
```

**Applied:**

```
handicapScore = scratchScore + handicapPerGame
totalHandicapPinfall = sum of (scratchScore[g] + handicapPerGame) for each game g
```

**Key rules:**

- If a bowler's average is at or above the base, their handicap is 0.
- The cap (`maximumHandicapPerGame`) limits how many pins of handicap a single game can receive.
- Handicap is division-specific — each division has its own base, percentage, and cap.
- Scratch divisions have `handicapPercentage = 0`.

---

### 5.2 Per-Squad Cashing and Advancing

All advancing and cashing calculations are **per division**. Each division within a squad is evaluated independently.

**Definitions:**

- `advancingRatio` = squad's `finalsRatio` if set, otherwise tournament's `finalsRatio` (1 in N advances)
- `cashRatio` = squad's `cashRatio` if set, otherwise tournament's `cashRatio` (1 in M cashes)
- `divisionEntries` = number of bowlers in this division who bowled this squad

**Calculation (always use `floor`, never round up):**

```
advancers = floor(divisionEntries / advancingRatio)
cashers    = floor(divisionEntries / cashRatio)
```

**Interpretation:**

- The top `advancers` bowlers **in the division** by total handicap pinfall advance to finals.
- The top `cashers` bowlers in the division receive a "Cashes" indicator.
- **Advancers always count as cashers** — they will earn money in finals. They fill the cashing spots first.
- Any cashing spots remaining after advancers are filled go to the next-highest non-advancing bowlers from that division in that squad.
- "Cashes" is a boolean status indicator on the results list — the application does not calculate or store a dollar payout amount.

**Tiebreaker:**
When two or more bowlers are tied on total handicap pinfall, the tiebreaker is the bowler's highest single game (scratch), then second-highest, then third-highest, and so on. This matches the existing software behavior.

**Example:**

> Tournament: cashRatio = 5, finalsRatio = 7
> Division with 30 entries in a squad:
>
> - `advancers = floor(30/7) = 4`
> - `cashers = floor(30/5) = 6`
> - 4 bowlers advance (they are also cashers).
> - 6 − 4 = 2 additional bowlers cash but do not advance.

**Example (27 entries in a division):**

> - `advancers = floor(27/7) = 3`
> - `cashers = floor(27/5) = 5`
> - 3 advance, 2 additional cash.

---

### 5.3 At-Large Advancement and Finals Seeding

All at-large and seeding calculations are **per division**. Each division maintains its own advancer count, at-large pool, and seeded list independently.

After all qualifying squads are complete, for **each division**:

**Step 1 — Determine the target advancer count for the division:**

```
totalDivisionEntries = sum of all entries in this division across all tournament squads
                       (a bowler who bowls 3 squads contributes 3 entries to this count)
targetAdvancers = floor(totalDivisionEntries / advancingRatio)
```

**Step 2 — Count actual advancers for the division:**

```
actualAdvancers = number of distinct bowlers in this division who advanced from any squad
```

**Step 3 — At-large round (if needed):**
If `actualAdvancers < targetAdvancers`, the shortfall is filled from the at-large pool:

- Take all bowlers in this division who have **not yet advanced** to finals.
- Sort them by their **best squad score** (highest single-squad total handicap pinfall across all the squads they bowled in this division).
- Take the top N to reach `targetAdvancers`. Apply tiebreaker rules (see 5.2) if needed.

**Bowlers who bowl multiple squads:**

- A bowler can bowl more than one squad and each appearance counts as a separate entry toward `totalDivisionEntries`.
- A bowler can only advance to finals **once** per division.
- If a bowler who has already advanced would place in the advancing spots of a later squad, they take a **cashing spot** in that squad instead — they consume a cash slot but do not displace the next bowler into an advancing position.
- For at-large purposes, a previously advanced bowler is ineligible and excluded from the at-large pool.

**Finals seeding (per division):**

- Finalists (squad advancers + at-large advancers) are seeded within their division by total handicap pinfall, highest to lowest.
- Seed 1 = highest pinfall in the division.
- Running finals (the bracket/eliminator format) is **out of scope** for this application. The app produces the seeded list; the TD runs finals manually.

---

### 5.4 Sweeper Rules

A **sweeper** is an optional side event held at the same venue on the same day as a tournament squad. It has:

- Its own separate entry fee
- Its own separate game count (may differ from the main tournament game count)
- Its own separate results and prize pool
- Optional division-specific brackets (e.g., Men's sweeper, Women's sweeper) via `SweeperDivision`

A bowler registers for a sweeper independently of the main tournament registration. Sweeper scores are tracked separately and do not affect main tournament results.

**Sweeper handicap — flat pins per bowler category (not percentage-based):**

Sweeper handicap uses a fixed number of bonus pins per game, not the USBC percentage formula used in the main tournament. The flat-pin amounts are defined per sweeper squad (or sweeper division) and applied uniformly:

| Bowler Category           | Bonus Pins per Game |
| ------------------------- | ------------------- |
| Handicap division bowlers | +10 pins/game       |
| 55+ and/or Women bowlers  | +6 pins/game        |
| Scratch division bowlers  | 0                   |

The sweeper squad (or sweeper division) configuration stores which flat-pin value applies to each category. Sweeper totals are calculated as:

```
sweeper_total = sum of (scratch_score[g] + flat_handicap_pins) for each game g
```

---

### 5.5 Super Sweeper Rules

The **Super Sweeper** is a bonus prize pool available to bowlers who bowl **all defined sweepers** in a tournament.

- At registration time, a bowler may opt into the Super Sweeper by setting `superSweeper = true` on their registration.
- To be eligible, the bowler must bowl every sweeper squad defined for the tournament.
- The Super Sweeper is a separate event with its own payout ratio defined by `tournament.superSweeperCashRatio` (1 in N bowlers cash).
- Super Sweeper scores = the combined total of all the bowler's sweeper scores across all sweepers they bowled.
- Results are calculated independently from the main tournament and from individual sweeper results.

---

### 5.6 Division Eligibility — Advisory Only

Eligibility enforcement is the Tournament Director's responsibility, not the application's. The application **does not block** registrations based on eligibility rules; it provides advisory information to help the TD make decisions.

When registering a bowler in a division, the UI should surface relevant data:

- **Age:** Display the bowler's age on the tournament `startDate` alongside the division's min/max age limits. Show a warning if out of range.
- **Average:** Display the bowler's declared average alongside the division's min/max average limits. Show a warning if out of range.
- **Gender:** Display the division's gender restriction and the bowler's gender. Show a warning if mismatched.

These are **warnings**, not errors. Staff can proceed with registration regardless.

**Division switching:**
A bowler may be moved from one division to another before competition begins. The application supports updating a registration's `divisionId` to a different division within the same tournament. The bowler's average may also be updated at the same time. Any existing squad assignments remain intact unless the divisions are incompatible with the squad.

**Duplicate prevention (hard rule — enforced):**
A bowler cannot be registered in the same division twice. `unique(bowlerId, divisionId)` is enforced at the database level and returns a 409 Conflict if violated.

**Forfeits:**
There is no forfeit tracking. If a bowler does not show up, the TD deletes the registration.

---

### 5.7 Payment Tracking

Payments are informational only — the application does **not** process payments. It records that a payment was received with:

- The amount
- A confirmation code (generated externally, e.g., from a payment processor receipt or cash receipt)
- A timestamp

There is no payment gateway integration.

---

### 5.8 IRS 1099 Reporting

- Social Security Numbers must be collected from bowlers who win prizes above the IRS threshold ($600 as of current rules).
- SSNs are **stored encrypted at rest** — see Section 10.
- Only users with the `director` role can view SSN data.
- The application should be able to generate or export a 1099-eligible list (bowler name, address, SSN, prize amount).

---

## 6. Feature Modules

### 6.1 Tournament Management

**Create/Edit Tournament:**

- All fields in the Tournament model (name, dates, entry fee, games, finalsRatio, cashRatio, bowlingCenter, superSweeperCashRatio)
- Mark tournament as completed

**Tournament List:**

- Table of all tournaments with name, dates, and completed status
- Filter by year / active

**Tournament Detail:**

- Summary of divisions, squads, total entries
- Links to all sub-sections

---

### 6.2 Division Management

- Add/edit/delete divisions for a tournament
- Division form includes all eligibility fields (age, average, gender) and handicap configuration
- Divisions are ordered by `number`

---

### 6.3 Bowler Management

**Bowler Search/List:**

- Search by name, USBC ID, email
- Paginated list

**Bowler Profile:**

- Create / edit all bowler fields
- SSN field is masked by default; director role required to view/edit
- View all tournaments the bowler is registered in

---

### 6.4 Registration Management

**Register a Bowler:**

- Select or search for an existing bowler (or create new)
- Select the division
- Enter declared average
- Display eligibility information (age, average, gender vs. division limits) as **advisory warnings only** — staff can register the bowler regardless; the TD makes the final eligibility call
- Set Super Sweeper opt-in flag

**Registration List (per tournament/division):**

- Show all registered bowlers with average, squads bowled, payment status

**Switch Division:**

- Move a bowler's registration to a different division within the same tournament
- Update the `divisionId` (and optionally the `average`) on the registration
- Show advisory eligibility info for the new division before confirming
- Existing squad assignments carry forward; warn the user if there are any

**Delete Registration:**

- Removes the bowler from the division and cascades to SquadRegistrations
- Warn if scores already exist for this bowler in any squad
- Used for both explicit removals and no-show/forfeit situations

**Append Registration (staff override):**

- Allows registering after the 3rd party system has closed (once the tournament is underway)
- Same form as the standard registration flow

---

### 6.5 Squad Management

**Create/Edit Squad:**

- Type (tournament or sweeper)
- Date and time
- Starting lane, number of lanes
- Max per pair
- Optional cash ratio override
- For sweepers: entry fee and games count

**Squad List (per tournament):**

- Show all squads with date, type, entries, completed status

**Delete Squad:**

- Only allowed if the squad has no entries and no scores

---

### 6.6 Lane Assignments

**Lane Assignment Screen:**

- Drag-and-drop interface
- Left panel: unassigned bowlers (registered in the division/squad)
- Right panel: lane pairs (derived from startingLane and numberOfLanes, step of 2)
- Each lane pair card shows assigned bowlers and has a maximum of `maxPerPair`
- Rules:
  - A bowler can only be on one lane pair
  - Cannot exceed `maxPerPair` per pair
  - Staff can manually override if needed

**Print Pairing Sheet:**

- Generate a printable/PDF view of all lane assignments
- Format: Lane X-Y | Bowler 1 | Bowler 2 | ... (up to maxPerPair)
- Include squad date, tournament name, division

---

### 6.7 Score Entry

**Score Entry Screen (per squad):**

- Table of bowlers assigned to the squad, one row per bowler
- Columns: one column per game (e.g., Game 1, Game 2, Game 3)
- Inline editable cells
- Auto-calculate totals (scratch and handicap) on entry
- Show handicap per game next to or below each score
- Role-gated: `staff` and `director` can edit; `viewer` sees read-only

**Validation:**

- Scores must be 0–300
- A game with 300 can exist for non-perfect games (no auto-validation beyond range)

---

### 6.8 Results & Standings

**Squad Results:**

- List of all bowlers in the squad, **grouped and sorted by division**, then sorted by total handicap pinfall (desc) within each division
- Columns: Rank (within division), Bowler, Average, Handicap/Game, Game scores, Total Scratch, Total Handicap, Status
- Status values: **Advances** | **Cashes** | _(blank)_
- "Cashes" is a boolean indicator — the app does not track dollar amounts for results
- Apply per-division advancing and cashing calculations (see 5.2)

**At-Large Results:**

- Calculated per division after all qualifying squads are complete
- Show the at-large pool for each division: all non-advanced bowlers sorted by their best squad total handicap pinfall
- Highlight which bowlers fill the at-large advancing spots to reach the division's target advancer count

**Finals Seeding:**

- Full seeded list per division (squad advancers + at-large advancers combined)
- Within each division, sorted by total handicap pinfall from highest to lowest; seed 1 = highest
- Running finals (bracket/eliminator format) is **out of scope** — the app produces the seeding sheet only
- Printable seeding sheet per division

**Sweeper Results:**

- Per sweeper squad, list bowlers by total pinfall (with any division breakdown if sweeper divisions are defined)
- Apply `cashRatio` for the sweeper squad

**Super Sweeper Results:**

- List all bowlers who bowled all sweepers and opted in
- Total combined sweeper pinfall
- Apply `superSweeperCashRatio` for cashing calculation

---

### 6.9 Payment Tracking

**Payment List (per registration):**

- Show all payments recorded for a registration
- Total paid vs. expected (based on entry fee + optional sweeper fees)

**Add Payment:**

- Amount
- Confirmation code (unique across the system)
- Timestamp auto-filled to now

---

### 6.10 Reporting / PDF Output

The following should be printable/exportable as PDF:

| Report                | Description                                                                        |
| --------------------- | ---------------------------------------------------------------------------------- |
| Pairing Sheet         | Lane assignments for a squad                                                       |
| Squad Results         | Scores and standings for a squad                                                   |
| Finals Seeding        | Seeded list of all finalists                                                       |
| Sweeper Results       | Results for a sweeper squad                                                        |
| Super Sweeper Results | Combined sweeper standings                                                         |
| Recap Sheet           | Score verification sheet (bowler names, game scores, totals) for staff to sign off |
| 1099 Report           | Bowlers with prize amounts and SSN (director-only)                                 |

---

## 7. API Specification

The Express.js API serves the React frontend and the third-party integration.

### Base URL

`/api/v1`

### Authentication Headers

- **JWT (all authenticated routes):** `Authorization: Bearer <token>`
- **API key (third-party integration only):** `x-api-key: <key>`

---

### 7.1 Auth Endpoints

| Method | Path           | Auth | Description                                        |
| ------ | -------------- | ---- | -------------------------------------------------- |
| POST   | `/auth/login`  | None | Returns JWT given valid credentials                |
| POST   | `/auth/logout` | JWT  | Invalidates session (if server-side sessions used) |

---

### 7.2 Tournament Endpoints

| Method | Path                        | Auth | Role     | Description                                  |
| ------ | --------------------------- | ---- | -------- | -------------------------------------------- |
| GET    | `/tournaments`              | JWT  | Any      | List all tournaments                         |
| GET    | `/tournaments/:id`          | JWT  | Any      | Get tournament with divisions and squads     |
| POST   | `/tournaments`              | JWT  | director | Create tournament                            |
| PUT    | `/tournaments/:id`          | JWT  | director | Update tournament                            |
| PATCH  | `/tournaments/:id/complete` | JWT  | director | Mark tournament completed                    |
| DELETE | `/tournaments/:id`          | JWT  | director | Delete tournament (only if no registrations) |

---

### 7.3 Division Endpoints

| Method | Path                         | Auth | Role     | Description                                |
| ------ | ---------------------------- | ---- | -------- | ------------------------------------------ |
| GET    | `/tournaments/:id/divisions` | JWT  | Any      | List divisions for a tournament            |
| POST   | `/tournaments/:id/divisions` | JWT  | director | Create division                            |
| PUT    | `/divisions/:id`             | JWT  | director | Update division                            |
| DELETE | `/divisions/:id`             | JWT  | director | Delete division (only if no registrations) |

---

### 7.4 Bowler Endpoints

| Method | Path               | Auth | Role            | Description                                      |
| ------ | ------------------ | ---- | --------------- | ------------------------------------------------ |
| GET    | `/bowlers`         | JWT  | Any             | Search/list bowlers (query: name, usbcId, email) |
| GET    | `/bowlers/:id`     | JWT  | Any             | Get bowler profile                               |
| POST   | `/bowlers`         | JWT  | staff, director | Create bowler                                    |
| PUT    | `/bowlers/:id`     | JWT  | staff, director | Update bowler                                    |
| GET    | `/bowlers/:id/ssn` | JWT  | director        | Retrieve decrypted SSN                           |

---

### 7.5 Registration Endpoints

| Method | Path                             | Auth           | Role                                | Description                                                     |
| ------ | -------------------------------- | -------------- | ----------------------------------- | --------------------------------------------------------------- |
| GET    | `/tournaments/:id/registrations` | JWT            | Any                                 | List all registrations for a tournament                         |
| GET    | `/divisions/:id/registrations`   | JWT            | Any                                 | List registrations for a division                               |
| GET    | `/registrations/:id`             | JWT            | Any                                 | Get single registration detail                                  |
| POST   | `/registrations`                 | JWT or API Key | staff, director, or service account | Create registration                                             |
| PUT    | `/registrations/:id`             | JWT            | staff, director                     | Update registration (average, superSweeper)                     |
| PATCH  | `/registrations/:id/division`    | JWT            | staff, director                     | Switch a bowler to a different division (division switching)    |
| DELETE | `/registrations/:id`             | JWT            | director                            | Delete registration (also used when a bowler forfeits/no-shows) |

**POST /registrations request body:**

```json
{
  "bowlerId": "...",
  "divisionId": "...",
  "average": 180,
  "superSweeper": false
}
```

---

### 7.6 Squad Endpoints

| Method | Path                      | Auth | Role            | Description                         |
| ------ | ------------------------- | ---- | --------------- | ----------------------------------- |
| GET    | `/tournaments/:id/squads` | JWT  | Any             | List squads for a tournament        |
| GET    | `/squads/:id`             | JWT  | Any             | Get squad detail with registrations |
| POST   | `/tournaments/:id/squads` | JWT  | staff, director | Create squad                        |
| PUT    | `/squads/:id`             | JWT  | staff, director | Update squad                        |
| PATCH  | `/squads/:id/complete`    | JWT  | staff, director | Mark squad completed                |
| DELETE | `/squads/:id`             | JWT  | director        | Delete squad                        |

---

### 7.7 Squad Registration (Lane Assignment) Endpoints

| Method | Path                                        | Auth | Role            | Description                          |
| ------ | ------------------------------------------- | ---- | --------------- | ------------------------------------ |
| GET    | `/squads/:id/assignments`                   | JWT  | Any             | Get all lane assignments for a squad |
| PUT    | `/squads/:id/assignments`                   | JWT  | staff, director | Replace full set of lane assignments |
| POST   | `/squads/:id/registrations/:registrationId` | JWT  | staff, director | Add a bowler to a squad              |
| DELETE | `/squads/:id/registrations/:registrationId` | JWT  | staff, director | Remove a bowler from a squad         |

---

### 7.8 Score Endpoints

| Method | Path                 | Auth | Role            | Description                    |
| ------ | -------------------- | ---- | --------------- | ------------------------------ |
| GET    | `/squads/:id/scores` | JWT  | Any             | Get all scores for a squad     |
| PUT    | `/squads/:id/scores` | JWT  | staff, director | Bulk update scores for a squad |

**PUT /squads/:id/scores request body:**

```json
{
  "scores": [
    { "bowlerId": "...", "game": 1, "score": 187 },
    { "bowlerId": "...", "game": 2, "score": 203 },
    ...
  ]
}
```

---

### 7.9 Results Endpoints

| Method | Path                               | Auth | Role | Description                                                |
| ------ | ---------------------------------- | ---- | ---- | ---------------------------------------------------------- |
| GET    | `/squads/:id/results`              | JWT  | Any  | Calculated standings for a squad (with advance/cash flags) |
| GET    | `/tournaments/:id/atlarge`         | JWT  | Any  | At-large pool and selections                               |
| GET    | `/tournaments/:id/seeding`         | JWT  | Any  | Finals seeding list                                        |
| GET    | `/tournaments/:id/sweeper-results` | JWT  | Any  | Super sweeper standings                                    |

---

### 7.10 Payment Endpoints

| Method | Path                          | Auth | Role            | Description                      |
| ------ | ----------------------------- | ---- | --------------- | -------------------------------- |
| GET    | `/registrations/:id/payments` | JWT  | Any             | List payments for a registration |
| POST   | `/registrations/:id/payments` | JWT  | staff, director | Record a payment                 |
| DELETE | `/payments/:id`               | JWT  | director        | Delete a payment record          |

---

### 7.11 PDF/Report Endpoints

| Method | Path                                   | Auth | Role     | Description                           |
| ------ | -------------------------------------- | ---- | -------- | ------------------------------------- |
| GET    | `/squads/:id/pairing-sheet.pdf`        | JWT  | Any      | Download pairing sheet PDF            |
| GET    | `/squads/:id/results.pdf`              | JWT  | Any      | Download squad results PDF            |
| GET    | `/squads/:id/recap.pdf`                | JWT  | Any      | Download recap/verification sheet PDF |
| GET    | `/tournaments/:id/seeding.pdf`         | JWT  | Any      | Download finals seeding PDF           |
| GET    | `/tournaments/:id/sweeper-results.pdf` | JWT  | Any      | Download sweeper results PDF          |
| GET    | `/tournaments/:id/1099-report.pdf`     | JWT  | director | Download 1099 report PDF              |

---

### Error Response Format

All API errors return:

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Human-readable description",
    "details": [ ... ]
  }
}
```

Standard HTTP status codes: 200, 201, 204, 400, 401, 403, 404, 409, 422, 429, 500.

---

## 8. Third-Party Integration

### Overview

A third-party registration system handles online bowler registration prior to the tournament. Once the tournament begins, that system closes and all registration is staff-managed.

### Integration Method

- **Push model:** The third-party system calls our API to create registrations.
- **Authentication:** API key via `x-api-key` header (pre-shared, configured per tournament or globally).
- **Endpoint:** `POST /api/v1/registrations`
- **Scope:** The service account associated with the API key has permission to create registrations and create bowlers only.

### Data Contract

The third-party system sends the same request body as a staff-created registration. If the bowler does not exist in our system by USBC ID or name match, a new bowler record is created.

### Cutoff

- Once the first qualifying squad for a tournament is marked as started (or a configurable cutoff time is set), the API key is invalidated for that tournament's registrations, and the third-party system can no longer push new entries.
- Subsequent registrations require staff login.

### Rate Limiting

- Authenticated (API key): 50 requests per 60 seconds
- Anonymous: not applicable (no anonymous write access)

---

## 9. Hosting & Deployment

### Platform

- **GreenGeeks** (EcoSite Premium or Managed VPS)
- Node.js apps are created via **cPanel → Software → Node.js**
- MariaDB database is the existing GreenGeeks-hosted instance

### Constraints

- GreenGeeks provides limited Node.js support — no support for debugging or deployment issues
- On Managed VPS, only a single Node.js version is available; confirm the available version before committing to Node.js features
- The app will run behind a reverse proxy (Apache, managed by cPanel); ensure the Express app does not bind to port 80/443 directly
- WebSocket support depends on the GreenGeeks plan — do not architect features that depend on persistent WebSocket connections until confirmed

### Recommended Architecture

```
Browser → Apache (cPanel) → reverse proxy → Node.js Express app (custom port)
                                          → MariaDB (same host)
```

### Environment Variables

Store all secrets in environment variables (not committed to source control):

- `DATABASE_URL` — MariaDB connection string
- `JWT_SECRET` — JWT signing secret
- `API_KEY` — Pre-shared key for third-party integration
- `ENCRYPTION_KEY` — Key for SSN encryption at rest
- Configure via cPanel's Node.js environment variable UI or a `.env` file excluded from git

### Process Management

- Use a process manager such as `pm2` if supported by the GreenGeeks plan
- cPanel's Node.js app management may handle restart automatically; verify behavior

---

## 10. Security Requirements

### Authentication & Authorization

- All routes except `GET /tournaments` (public tournament list, if desired) require a valid JWT
- JWT expiry: 8 hours (configurable); no refresh token required for MVP
- Role is embedded in JWT claims; validated server-side on every request

### SSN Encryption

- Social Security Numbers are **never stored in plaintext**
- Encrypt using AES-256 (e.g., Node.js `crypto` module with `aes-256-gcm`)
- The encryption key is stored in an environment variable (`ENCRYPTION_KEY`), never in source control
- The SSN field is masked in API responses by default — a separate director-only endpoint decrypts and returns the value
- Log access to SSN data (who accessed it, when)

### Input Validation

- All API inputs validated with Zod or Joi before processing
- SQL injection prevention via ORM parameterized queries (never raw interpolated strings)
- XSS prevention: React handles this on the frontend by default; sanitize any fields rendered as raw HTML

### Rate Limiting

- Apply rate limiting on all API routes (e.g., `express-rate-limit`)
- Stricter limits on auth endpoints to prevent brute force

### HTTPS

- Enforce HTTPS. GreenGeeks provides SSL via Let's Encrypt through cPanel.
- Redirect all HTTP to HTTPS at the Apache level.

### Sensitive Data

- Do not log SSNs, passwords, or API keys in application logs
- Confirmation codes in payments are not secret but should not be overexposed

---

## 11. MVP Scope

All four modules below are in scope for the initial release:

### MVP Modules

| Module                                  | Details                                                                                                                            |
| --------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| Tournament & Division Setup             | Create/edit tournaments with all configuration; add/edit divisions with handicap and eligibility rules                             |
| Bowler Registration & Squad Assignments | Bowler profiles, registration in divisions, squad creation, lane assignment drag-and-drop, pairing sheet PDF                       |
| Score Entry & Results                   | Score entry by staff, handicap calculation display, squad standings with advance/cash status, at-large calculation, finals seeding |
| Sweeper & Payment Tracking              | Sweeper squad creation, sweeper results, Super Sweeper standings, payment recording per registration                               |

### Post-MVP Considerations

- PDF generation polish (branding, layout)
- 1099 report export
- User management UI (creating staff accounts)
- Audit log / history of changes
- Tournament series / multi-tournament reporting
- Mobile-optimized score entry (currently planned as responsive web)

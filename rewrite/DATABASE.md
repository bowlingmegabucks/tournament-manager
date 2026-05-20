# BowlingMegaBucks — Database Reference

> **Engine:** MariaDB (GreenGeeks-hosted)
> **ORM:** Sequelize or Prisma (MariaDB dialect) — TBD
> **Connection:** `DATABASE_URL` environment variable (never in source control)

---

## DDL (Current Production Schema)

```sql
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Tournaments` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Start` datetime(6) NOT NULL,
    `End` datetime(6) NOT NULL,
    `EntryFee` decimal(5,2) NOT NULL,
    `Games` smallint NOT NULL,
    `FinalsRatio` decimal(3,1) NOT NULL,
    `CashRatio` decimal(3,1) NOT NULL,
    `BowlingCenter` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Completed` tinyint(1) NOT NULL,
    `SuperSweperCashRatio` decimal(3,1) NOT NULL DEFAULT 0.0,  -- NOTE: typo in column name (Sweper), do not correct in new schema without a migration
    CONSTRAINT `PK_Tournaments` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Divisions` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Number` smallint NOT NULL,
    `TournamentId` char(36) COLLATE ascii_general_ci NOT NULL,
    `MinimumAge` smallint NULL,
    `MaximumAge` smallint NULL,
    `MinimumAverage` int NULL,
    `MaximumAverage` int NULL,
    `HandicapPercentage` decimal(3,2) NULL,
    `HandicapBase` int NULL,
    `MaximumHandicapPerGame` int NULL,
    `Gender` int NULL,  -- enum: see Gender Enum section below
    CONSTRAINT `PK_Divisions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Divisions_Tournaments_TournamentId` FOREIGN KEY (`TournamentId`) REFERENCES `Tournaments` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Squads` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `TournamentId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CashRatio` decimal(3,1) NULL,
    `Date` datetime(6) NOT NULL,
    `MaxPerPair` smallint NOT NULL,
    `Complete` tinyint(1) NOT NULL,              -- NOTE: column is "Complete" not "Completed" (differs from Tournaments)
    `SquadType` int NOT NULL,                    -- discriminator: 0 = tournament squad, 1 = sweeper squad
    `SweeperEntryFee` decimal(5,2) NULL,         -- sweeper squads only (SquadType=1)
    `Games` smallint NULL,                       -- sweeper squads only (SquadType=1)
    `FinalsRatio` decimal(3,1) NULL,             -- tournament squads only; squad-level override of Tournament.FinalsRatio
    `NumberOfLanes` smallint NOT NULL DEFAULT 0,
    `StartingLane` smallint NOT NULL DEFAULT 0,
    `SquadEntryFee` decimal(5,2) NULL,           -- tournament squads only; overrides Tournament.EntryFee for this squad
    CONSTRAINT `PK_Squads` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Squads_Tournaments_TournamentId` FOREIGN KEY (`TournamentId`) REFERENCES `Tournaments` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SweeperDivision` (
    `SweeperId` char(36) COLLATE ascii_general_ci NOT NULL,  -- FK to Squads.Id where SquadType=1
    `DivisionId` char(36) COLLATE ascii_general_ci NOT NULL,
    `BonusPinsPerGame` int NULL,                             -- flat bonus pins per game for this division in this sweeper
    CONSTRAINT `PK_SweeperDivision` PRIMARY KEY (`SweeperId`, `DivisionId`),
    CONSTRAINT `FK_SweeperDivision_Divisions_DivisionId` FOREIGN KEY (`DivisionId`) REFERENCES `Divisions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SweeperDivision_Squads_SweeperId` FOREIGN KEY (`SweeperId`) REFERENCES `Squads` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Bowlers` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `MiddleInitial` char(1) CHARACTER SET utf8mb4 NOT NULL,  -- empty string when absent (NOT NULL, no nulls in DB)
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Suffix` longtext CHARACTER SET utf8mb4 NOT NULL,        -- empty string when absent
    `StreetAddress` longtext CHARACTER SET utf8mb4 NOT NULL, -- empty string when absent
    `CityAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `StateAddress` char(2) CHARACTER SET utf8mb4 NOT NULL,
    `ZipCode` char(9) CHARACTER SET utf8mb4 NOT NULL,
    `EmailAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PhoneNumber` char(10) CHARACTER SET utf8mb4 NOT NULL,
    `USBCId` longtext CHARACTER SET utf8mb4 NOT NULL,        -- empty string when absent
    `DateOfBirth` datetime(6) NULL,
    `Gender` int NULL,                                       -- enum: see Gender Enum section below
    `SocialSecurityNumber` longtext CHARACTER SET utf8mb4 NOT NULL,  -- AES-256-GCM encrypted; empty string when not provided
    CONSTRAINT `PK_Bowlers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Registrations` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `BowlerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `DivisionId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Average` int NULL,
    `SuperSweeper` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Registrations` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Registrations_Bowlers_BowlerId` FOREIGN KEY (`BowlerId`) REFERENCES `Bowlers` (`Id`),        -- no cascade
    CONSTRAINT `FK_Registrations_Divisions_DivisionId` FOREIGN KEY (`DivisionId`) REFERENCES `Divisions` (`Id`) -- no cascade
) CHARACTER SET=utf8mb4;

CREATE TABLE `SquadRegistration` (
    `RegistrationId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SquadId` char(36) COLLATE ascii_general_ci NOT NULL,
    `LaneAssignment` varchar(3) CHARACTER SET utf8mb4 NOT NULL DEFAULT '',  -- e.g. "1", "15", "39"; empty = unassigned
    CONSTRAINT `PK_SquadRegistration` PRIMARY KEY (`RegistrationId`, `SquadId`),
    CONSTRAINT `FK_SquadRegistration_Registrations_RegistrationId` FOREIGN KEY (`RegistrationId`) REFERENCES `Registrations` (`Id`),  -- no cascade
    CONSTRAINT `FK_SquadRegistration_Squads_SquadId` FOREIGN KEY (`SquadId`) REFERENCES `Squads` (`Id`)                               -- no cascade
) CHARACTER SET=utf8mb4;

CREATE TABLE `SquadScores` (
    `BowlerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SquadId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Game` smallint NOT NULL,   -- 1-based
    `Score` int NOT NULL,       -- scratch (raw) pins; 0–300
    CONSTRAINT `PK_SquadScores` PRIMARY KEY (`BowlerId`, `SquadId`, `Game`),
    CONSTRAINT `FK_SquadScores_Bowlers_BowlerId` FOREIGN KEY (`BowlerId`) REFERENCES `Bowlers` (`Id`),  -- no cascade
    CONSTRAINT `FK_SquadScores_Squads_SquadId` FOREIGN KEY (`SquadId`) REFERENCES `Squads` (`Id`)       -- no cascade
) CHARACTER SET=utf8mb4;

CREATE TABLE `Payments` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAtUtc` datetime(6) NOT NULL,
    `RegistrationId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ConfirmationCode` varchar(43) CHARACTER SET utf8mb4 NOT NULL,  -- unique; 43 chars = base64url-encoded UUID
    `Amount` decimal(5,2) NOT NULL,
    CONSTRAINT `PK_Payments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Payments_Registrations_RegistrationId` FOREIGN KEY (`RegistrationId`) REFERENCES `Registrations` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

-- Indexes
CREATE INDEX `IX_Divisions_TournamentId` ON `Divisions` (`TournamentId`);
CREATE INDEX `IX_Squads_TournamentId` ON `Squads` (`TournamentId`);
CREATE INDEX `IX_SweeperDivision_DivisionId` ON `SweeperDivision` (`DivisionId`);
CREATE INDEX `IX_Registrations_DivisionId` ON `Registrations` (`DivisionId`);
CREATE UNIQUE INDEX `IX_Registrations_BowlerId_DivisionId` ON `Registrations` (`BowlerId`, `DivisionId`);
CREATE INDEX `IX_SquadRegistration_SquadId` ON `SquadRegistration` (`SquadId`);
CREATE INDEX `IX_SquadScores_SquadId` ON `SquadScores` (`SquadId`);
CREATE UNIQUE INDEX `IX_Payments_ConfirmationCode` ON `Payments` (`ConfirmationCode`);
CREATE INDEX `IX_Payments_RegistrationId` ON `Payments` (`RegistrationId`);
```

---

## Gender Enum

`Gender` is stored as `int` in both `Bowlers.Gender` and `Divisions.Gender`. Mapping from the .NET `Models.Gender` enum:

| Value | Meaning              |
| ----- | -------------------- |
| `0`   | Male (`M`)           |
| `1`   | Female (`F`)         |
| `2`   | Any (divisions only) |

`NULL` in `Bowlers.Gender` = not specified. `NULL` in `Divisions.Gender` = no restriction (same as `Any`).

---

## Schema Quirks — Read Before Writing ORM Models

These are real divergences from what REWRITE_SPEC describes. Handle them explicitly.

### 1. Typo: `SuperSweperCashRatio` (one 'e' in Sweper)

The column name on `Tournaments` is misspelled. Map it as-is in the ORM; do not silently rename without a migration.

### 2. Single-table inheritance for Squads

`Tournaments`, `TournamentSquad`, and `SweeperSquad` are all stored in a single `Squads` table with a `SquadType` int discriminator. Columns are conditionally applicable:

- `SquadEntryFee`, `FinalsRatio` — tournament squads only (`SquadType = 0`)
- `SweeperEntryFee`, `Games` — sweeper squads only (`SquadType = 1`)
  NULL in a non-applicable column is normal; do not treat it as missing data.

### 3. "Optional" fields stored as empty string, not NULL

Many `Bowlers` text fields (`MiddleInitial`, `Suffix`, `StreetAddress`, etc.) are `NOT NULL` in the DB but store `''` when absent. The new API must treat empty string as absent when reading and write empty string (not NULL) when a value is not provided.

### 4. `Squads.Complete` vs `Tournaments.Completed`

The completed flag is named `Complete` on squads and `Completed` on tournaments. Map accordingly.

### 5. Lane assignment is on `SquadRegistration`, not a separate table

`SquadRegistration.LaneAssignment` is a `varchar(3)` storing the starting lane number (left lane of the pair) as a string. Empty string = unassigned. There is no separate lane assignment table.

### 6. No cascade delete on Registration FKs

`Registrations → Bowlers` and `Registrations → Divisions` foreign keys have **no cascade action**. Similarly `SquadRegistration` and `SquadScores` FKs have no cascade. Deleting a bowler or division will fail if registrations or scores exist. Enforce deletion order in service code.

### 7. `FinalsRatio` appears on both `Tournaments` and `Squads`

`Squads.FinalsRatio` is a nullable squad-level override. REWRITE_SPEC §4.5 does not mention this — it was present in the original schema. Preserve it; the results logic should prefer the squad value over the tournament value when non-null.

### 8. `Payments` table was added in a migration not present in the .cs migration files

Migration `20250811162608_PaymentRegistrationInformation` created the `Payments` table. The `.cs` file may not exist in the repo — this migration may have been applied directly. Do not attempt to re-create it.

---

## Migration Strategy

- The existing schema is the baseline — the new Node.js ORM migrates forward from here.
- Migrations are forward-only; no down migrations required for MVP.
- When renaming fields for the new API (e.g., `SuperSweperCashRatio` → `superSweeperCashRatio`), map in the ORM layer rather than altering the column unless a data migration is planned.

---

## Environment Variables

| Variable         | Purpose                                          |
| ---------------- | ------------------------------------------------ |
| `DATABASE_URL`   | MariaDB connection string                        |
| `ENCRYPTION_KEY` | AES-256-GCM key for SSN encryption at rest       |
| `JWT_SECRET`     | JWT signing secret                               |
| `API_KEY`        | Pre-shared key for third-party registration push |

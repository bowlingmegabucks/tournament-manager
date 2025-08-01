﻿CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

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
    `Gender` int NULL,
    CONSTRAINT `PK_Divisions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Divisions_Tournaments_TournamentId` FOREIGN KEY (`TournamentId`) REFERENCES `Tournaments` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Squads` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `TournamentId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CashRatio` decimal(3,1) NULL,
    `Date` datetime(6) NOT NULL,
    `MaxPerPair` smallint NOT NULL,
    `Complete` tinyint(1) NOT NULL,
    `SquadType` int NOT NULL,
    `EntryFee` decimal(5,2) NULL,
    `Games` smallint NULL,
    `FinalsRatio` decimal(3,1) NULL,
    CONSTRAINT `PK_Squads` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Squads_Tournaments_TournamentId` FOREIGN KEY (`TournamentId`) REFERENCES `Tournaments` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SweeperDivision` (
    `SweeperId` char(36) COLLATE ascii_general_ci NOT NULL,
    `DivisionId` char(36) COLLATE ascii_general_ci NOT NULL,
    `BonusPinsPerGame` int NULL,
    CONSTRAINT `PK_SweeperDivision` PRIMARY KEY (`SweeperId`, `DivisionId`),
    CONSTRAINT `FK_SweeperDivision_Divisions_DivisionId` FOREIGN KEY (`DivisionId`) REFERENCES `Divisions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SweeperDivision_Squads_SweeperId` FOREIGN KEY (`SweeperId`) REFERENCES `Squads` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Divisions_TournamentId` ON `Divisions` (`TournamentId`);

CREATE INDEX `IX_Squads_TournamentId` ON `Squads` (`TournamentId`);

CREATE INDEX `IX_SweeperDivision_DivisionId` ON `SweeperDivision` (`DivisionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220527183020_TournamentEntities', '8.0.11');

COMMIT;

START TRANSACTION;

CREATE TABLE `Bowlers` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `MiddleInitial` char(1) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Suffix` longtext CHARACTER SET utf8mb4 NOT NULL,
    `StreetAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CityAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `StateAddress` char(2) CHARACTER SET utf8mb4 NOT NULL,
    `ZipCode` char(9) CHARACTER SET utf8mb4 NOT NULL,
    `EmailAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PhoneNumber` char(10) CHARACTER SET utf8mb4 NOT NULL,
    `USBCId` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DateOfBirth` datetime(6) NULL,
    `Gender` int NULL,
    CONSTRAINT `PK_Bowlers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220615233907_BowlerEntity', '8.0.11');

COMMIT;

START TRANSACTION;

ALTER TABLE `Squads` ADD `NumberOfLanes` smallint NOT NULL DEFAULT 0;

ALTER TABLE `Squads` ADD `StartingLane` smallint NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220622145345_StartingAndNumberOfLanes', '8.0.11');

COMMIT;

START TRANSACTION;

CREATE TABLE `Registrations` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `BowlerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `DivisionId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Average` int NULL,
    `SuperSweeper` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Registrations` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Registrations_Bowlers_BowlerId` FOREIGN KEY (`BowlerId`) REFERENCES `Bowlers` (`Id`),
    CONSTRAINT `FK_Registrations_Divisions_DivisionId` FOREIGN KEY (`DivisionId`) REFERENCES `Divisions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SquadRegistration` (
    `RegistrationId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SquadId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_SquadRegistration` PRIMARY KEY (`RegistrationId`, `SquadId`),
    CONSTRAINT `FK_SquadRegistration_Registrations_RegistrationId` FOREIGN KEY (`RegistrationId`) REFERENCES `Registrations` (`Id`),
    CONSTRAINT `FK_SquadRegistration_Squads_SquadId` FOREIGN KEY (`SquadId`) REFERENCES `Squads` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Registrations_DivisionId` ON `Registrations` (`DivisionId`);

CREATE INDEX `IX_SquadRegistration_SquadId` ON `SquadRegistration` (`SquadId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220816235947_AddRegistration', '8.0.11');

COMMIT;

START TRANSACTION;

ALTER TABLE `SquadRegistration` ADD `LaneAssignment` varchar(3) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220831173955_SquadLaneAssignment', '8.0.11');

COMMIT;

START TRANSACTION;

CREATE TABLE `SquadScores` (
    `BowlerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SquadId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Game` smallint NOT NULL,
    `Score` int NOT NULL,
    CONSTRAINT `PK_SquadScores` PRIMARY KEY (`BowlerId`, `SquadId`, `Game`),
    CONSTRAINT `FK_SquadScores_Bowlers_BowlerId` FOREIGN KEY (`BowlerId`) REFERENCES `Bowlers` (`Id`),
    CONSTRAINT `FK_SquadScores_Squads_SquadId` FOREIGN KEY (`SquadId`) REFERENCES `Squads` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_SquadScores_SquadId` ON `SquadScores` (`SquadId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221017125314_SquadScores', '8.0.11');

COMMIT;

START TRANSACTION;

ALTER TABLE `Tournaments` ADD `SuperSweperCashRatio` decimal(3,1) NOT NULL DEFAULT 0.0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221020195709_SuperSweeperCashRatio', '8.0.11');

COMMIT;

START TRANSACTION;

ALTER TABLE `Bowlers` ADD `SocialSecurityNumber` longtext CHARACTER SET utf8mb4 NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230316131029_BowlerSSN', '8.0.11');

COMMIT;

START TRANSACTION;

ALTER TABLE `Squads` RENAME COLUMN `EntryFee` TO `SweeperEntryFee`;

ALTER TABLE `Squads` ADD `SquadEntryFee` decimal(5,2) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230802154109_TournamentSquadEntryFee', '8.0.11');

COMMIT;

START TRANSACTION;

CREATE UNIQUE INDEX `IX_Registrations_BowlerId_DivisionId` ON `Registrations` (`BowlerId`, `DivisionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241128140948_RegistrationAltKeyToIndex', '8.0.11');

COMMIT;


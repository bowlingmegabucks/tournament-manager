SELECT COUNT(*) as 'Entries'
FROM SquadRegistration
WHERE RegistrationId IN (
    SELECT Id
    FROM Registrations
    WHERE DivisionId IN (
        SELECT Id
        FROM Divisions
        WHERE TournamentId = (
            SELECT Id
            FROM Tournaments
            WHERE YEAR(`START`) = 0000 -- Year of Tournament
        )
    )
);

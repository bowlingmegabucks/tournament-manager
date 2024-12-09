-- Unique Bowlers for a Tournament Given a Theoretical Age Division
-- This excludes any handicap bowlers
SELECT CASE
         WHEN Timestampdiff(year, dateofbirth, Curdate()) <= 00 THEN
         'Age 00 and under'
         WHEN Timestampdiff(year, dateofbirth, Curdate()) BETWEEN 00 AND 00 THEN
         'Age 00 to 00'
         WHEN Timestampdiff(year, dateofbirth, Curdate()) >= 00
               OR gender = 1 THEN 'Age 00 and up (or Female)'
       end      AS AgeGroup,
       Count(*) AS BowlerCount
FROM   bowlers
WHERE  id IN (SELECT bowlerid
              FROM   registrations
              WHERE  id IN (SELECT registrationid
                            FROM   squadregistration
                            WHERE  registrationid IN (SELECT id
                                                      FROM   registrations
                                                      WHERE
                                   divisionid IN (SELECT id
                                                  FROM   divisions
                                                  WHERE
                                   handicapbase IS NULL
                                   AND tournamentid =
                                       (SELECT id
                                        FROM
                                       tournaments
                                       WHERE
                                       Year(`start`) = 0000
                                       -- Year of Tournament
                                       )))))
GROUP  BY agegroup
ORDER  BY agegroup; 

SELECT 
    TIMESTAMPDIFF(YEAR, Bowler.DateOfBirth, CURDATE()) AS Age,
    COUNT(*) AS Count
FROM 
    Registrations AS Reg
JOIN 
    Bowlers AS Bowler ON Reg.BowlerId = Bowler.Id
WHERE 
    Reg.DivisionId IN () -- get the divisions for the tournament
GROUP BY 
    Age
ORDER BY 
    Age;

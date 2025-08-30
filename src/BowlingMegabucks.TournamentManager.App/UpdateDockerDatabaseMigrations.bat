@echo off
echo Updating database migrations for Tournament Manager...
echo.

REM Read connection string from appsettings.Docker.Development.json and modify host and port
echo Reading connection string from appsettings.Docker.Development.json...

REM Use PowerShell to read JSON and replace host and port
for /f "delims=" %%i in ('powershell -Command "$json = Get-Content '..\BowlingMegabucks.TournamentManager.Api\appsettings.Docker.Development.json' | ConvertFrom-Json; $connectionString = $json.'ConnectionStrings:TournamentManager'; $connectionString = $connectionString -replace 'Server=database', 'Server=localhost'; $connectionString = $connectionString -replace 'Port=3306', 'Port=23306'; $connectionString"') do set "ConnectionStrings__TournamentManager=%%i"

echo Connection string loaded with host changed to localhost and port updated to 23306
echo.

REM Navigate to the Infrastructure project directory where the DbContext is located
cd /d "%~dp0..\BowlingMegabucks.TournamentManager.Infrastructure"

echo Running Entity Framework database update...
echo.

REM Run the EF database update command
dotnet ef database update --project . --startup-project ..\BowlingMegabucks.TournamentManager.Api

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ✓ Database migrations completed successfully!
) else (
    echo.
    echo ✗ Database migration failed with error code %ERRORLEVEL%
    echo Please check the connection string and ensure Docker database is running.
)

echo.
pause

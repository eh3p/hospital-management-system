@echo off
echo Starting Hospital Management System...
echo.
echo Application will be available at: http://localhost:5000
echo.
echo Press Ctrl+C to stop the application
echo.

cd /d "%~dp0"
dotnet run --urls "http://localhost:5000" --environment Development

pause 
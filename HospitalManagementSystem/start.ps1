<#
.HOSPITAL MANAGEMENT SYSTEM STARTUP SCRIPT
.VERSION 2.0
#>

Write-Host "🏥 Starting Hospital Management System..." -ForegroundColor Green
Write-Host ""
Write-Host "API Endpoint: http://localhost:5000" -ForegroundColor Yellow
Write-Host "Swagger UI: http://localhost:5000/swagger" -ForegroundColor Yellow
Write-Host ""
Write-Host "Press Ctrl+C to stop" -ForegroundColor Cyan
Write-Host ""

# Set environment variables
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:ASPNETCORE_URLS="http://localhost:5000"

# Cleanup previous processes
try {
    $process = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue | 
              Where-Object { $_.MainWindowTitle -like "*HospitalManagementSystem*" }
    if ($process) {
        Write-Host "Stopping existing process..." -ForegroundColor Yellow
        $process | Stop-Process -Force
        Start-Sleep -Seconds 2
    }
} catch {
    Write-Host "Cleanup warning: $_" -ForegroundColor Yellow
}

# Start application
try {
    dotnet run --no-build
} catch {
    Write-Host "Startup failed: $_" -ForegroundColor Red
    Write-Host "Try these solutions:" -ForegroundColor Yellow
    Write-Host "1. Run 'dotnet build' manually" -ForegroundColor Cyan
    Write-Host "2. Check database connection settings" -ForegroundColor Cyan
    Write-Host "3. Verify SQL Server is running" -ForegroundColor Cyan
    Pause
    exit 1
}
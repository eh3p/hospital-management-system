# Hospital Management System Startup Script
Write-Host "🏥 Starting Hospital Management System..." -ForegroundColor Green
Write-Host ""
Write-Host "Application will be available at: http://localhost:5000" -ForegroundColor Yellow
Write-Host ""
Write-Host "Press Ctrl+C to stop the application" -ForegroundColor Cyan
Write-Host ""

# Set the working directory
Set-Location $PSScriptRoot

# Kill any existing dotnet processes on port 5000
try {
    $processes = Get-NetTCPConnection -LocalPort 5000 -ErrorAction SilentlyContinue | ForEach-Object { Get-Process -Id $_.OwningProcess -ErrorAction SilentlyContinue }
    if ($processes) {
        Write-Host "Stopping existing processes on port 5000..." -ForegroundColor Yellow
        $processes | Stop-Process -Force -ErrorAction SilentlyContinue
        Start-Sleep -Seconds 2
    }
} catch {
    Write-Host "No existing processes found on port 5000" -ForegroundColor Gray
}

# Start the application
try {
    Write-Host "Starting application..." -ForegroundColor Green
    dotnet run --urls "http://localhost:5000" --environment Development
} catch {
    Write-Host "Error starting application: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Press any key to exit..." -ForegroundColor Yellow
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
} 
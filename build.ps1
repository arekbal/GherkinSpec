
$ForegroundColor = "Orange"

$PROJECT_DIR = $env:PROJECT_DIR
$BUILD_LOGGER = $env:BUILD_LOGGER

if (-not $PROJECT_DIR) 
{ 
  $PROJECT_DIR = $PSScriptRoot 
}

Write ""

Write-Host $ExecutionContext.InvokeCommand.ExpandString('PROJECT_DIR = $PROJECT_DIR') -ForegroundColor $ForegroundColor

Write ""
$clean = 'dotnet clean "$PROJECT_DIR\src\GherkinSpec.sln" -v m'
Write-Host 'CLEAN' -ForegroundColor $ForegroundColor
Write-Host $clean -ForegroundColor $ForegroundColor
Write ""
iex $clean

Write ""
$build = 'dotnet build "$PROJECT_DIR\src\GherkinSpec.sln" -v m'
Write-Host 'BUILD' -ForegroundColor $ForegroundColor
Write-Host $build -ForegroundColor $ForegroundColor
Write ""
iex $build

Write ""
$build = 'dotnet test "$PROJECT_DIR\src\GherkinSpec.sln" -v m'
Write-Host 'TESTS' -ForegroundColor $ForegroundColor
Write-Host $build -ForegroundColor $ForegroundColor
Write ""
iex $build

Write ""
Write-Host 'DONE' -ForegroundColor $ForegroundColor
Write ""

[Console]::ResetColor()
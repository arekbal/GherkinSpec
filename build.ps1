
$ForegroundColor = "Yellow"

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
Write-Host $clean -ForegroundColor $ForegroundColor
Write ""
iex $clean

$build = 'dotnet build "$PROJECT_DIR\src\GherkinSpec.sln" -v m'

# if ($BUILD_LOGGER) 
# { 
  # $build = $build + ' -logger:' + '"$BUILD_LOGGER"'
# } 
Write-Host $build -ForegroundColor $ForegroundColor
Write ""
iex $build
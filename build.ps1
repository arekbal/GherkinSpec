
$ForegroundColor = "Yellow"

function Exec-Cmd 
{
  param([String]$title, [String]$cmd )
  
  Write ""
  Write-Host $title -ForegroundColor $ForegroundColor
  Write-Host $cmd -ForegroundColor $ForegroundColor
  Write ""
  iex $cmd
}


$PROJECT_DIR = $env:PROJECT_DIR
$BUILD_LOGGER = $env:BUILD_LOGGER

if (-not $PROJECT_DIR) 
{ 
  $PROJECT_DIR = $PSScriptRoot 
}

Write ""

Write-Host $ExecutionContext.InvokeCommand.ExpandString('PROJECT_DIR = $PROJECT_DIR') -ForegroundColor $ForegroundColor

Exec-Cmd 'GIT VERSION INSTALL' 'choco install GitVersion.Portable'

Exec-Cmd 'GIT VERSION' 'GitVersion "$PROJECT_DIR" -updateassemblyinfo'

Exec-Cmd 'CLEAN' 'dotnet clean "$PROJECT_DIR\src\GherkinSpec.sln" -v m'

Exec-Cmd 'BUILD' 'dotnet build "$PROJECT_DIR\src\GherkinSpec.sln" -v m'

Exec-Cmd 'TESTS' 'dotnet test "$PROJECT_DIR\src\GherkinSpec.sln" -v m'

Write ""
Write-Host 'DONE' -ForegroundColor $ForegroundColor
Write ""

[Console]::ResetColor()

trap [System.Exception]
{
    Write-Error 'ERROR' -ForegroundColor 'Red'
	Write-Error $_.Exception	
    exit 
}
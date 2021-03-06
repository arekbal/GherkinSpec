Set-StrictMode -Version latest 
$ErrorActionPreference = "Stop"

$InfoColor = "Yellow"
$ErrorColor = "Red"

function Exec-Cmd 
{
  param([String]$title, [String]$cmd, [bool]$ignore)
  
  Write ""
  Write-Host $title -ForegroundColor $InfoColor
  Write-Host $cmd -ForegroundColor $InfoColor
  Write ""
  if ($ignore -ne $true)
  {
	  iex $cmd
	  
	  [Console]::ResetColor()
	  
	  if ($LastExitCode -ne 0)
	  {
		##throw 'failed running the command:"$cmd"'
		$message =  "ERROR: Failed running the command: '$cmd', returns code $LastExitCode"
		Write-Host $message -ForegroundColor $ErrorColor
		exit $LastExitCode
	  }
  }
  
  trap [System.Exception]
  {
    #Write-Error 'ERROR'
	Write-Error $_.Exception	
    exit 1
  }
}

function Print
{
  param([String]$title)
  
  Write ""
  Write-Host $title -ForegroundColor $InfoColor
  Write "" 
}

function Print-Var
{
  param([String]$title)
  
  $var = Get-Variable -Name $title -ValueOnly  
  
  Write-Host $ExecutionContext.InvokeCommand.ExpandString('$title = $var') -ForegroundColor $InfoColor
}

$PROJECT_DIR = if ($env:PROJECT_DIR) { $env:PROJECT_DIR } else { $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath('.\') } #$PSScriptRoot
$BUILD_CONFIGURATION = if ($env:BUILD_CONFIGURATION) { $env:BUILD_CONFIGURATION } else { 'Release' }
$BUILD_LOGGER = $env:BUILD_LOGGER

Write ""
Print-Var 'PROJECT_DIR'
Print-Var 'BUILD_CONFIGURATION'
Print-Var 'BUILD_LOGGER'

trap [System.Exception]
{
    Write-Host 'ERROR' -ForegroundColor $ErrorColor
	Write-Error $_.Exception	
    exit 1
}
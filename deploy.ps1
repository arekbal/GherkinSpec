﻿
. .\scripts\_include.ps1

$NUGET_SERVER = if ($env:NUGET_SERVER) { $env:NUGET_SERVER } else { 'https://api.nuget.org/v3/index.json' } # 
$NUGET_APIKEY = $env:NUGET_APIKEY
 
Print-Var 'NUGET_SERVER'
Print-Var 'NUGET_APIKEY'
 
Exec-Cmd 'GIT_VERSION INSTALL' 'choco install GitVersion.Portable'

Exec-Cmd 'GIT_VERSION' 'gitversion "$PROJECT_DIR" -updateassemblyinfo'

Exec-Cmd 'CLEAN' 'dotnet clean "$PROJECT_DIR\src\GherkinSpec.sln" -v m'

Exec-Cmd 'BUILD' 'dotnet build "$PROJECT_DIR\src\GherkinSpec.sln" -c $BUILD_CONFIGURATION -v m'

#Exec-Cmd 'TESTS' 'dotnet test "$PROJECT_DIR\src\GherkinSpec.sln" -a "$PROJECT_DIR\src\GherkinSpec.Tests\bin\$BUILD_CONFIGURATION\netcoreapp2.0" -c $BUILD_CONFIGURATION --no-build -v m' #This crap returns 1 because of being unable to understand NUnit tests even though it uses test adapter

Exec-Cmd 'TESTS' 'dotnet vstest "$PROJECT_DIR\src\GherkinSpec.Tests\bin\$BUILD_CONFIGURATION\netcoreapp2.0\GherkinSpec.Tests.dll" /TestAdapterPath:"$PROJECT_DIR\src\GherkinSpec.Tests\bin\$BUILD_CONFIGURATION\netcoreapp2.0" --Parallel'

Exec-Cmd 'GET_GIT_VERSION' 'gitversion -showvariable SemVer' -ignore $true
$VERSION = iex 'gitversion -showvariable SemVer'
Print-Var 'VERSION'

function Nuget-PP
{
  param([String]$proj)
  
  Exec-Cmd "PACK $proj" 'dotnet pack "$PROJECT_DIR\src\$proj" --no-dependencies /p:PackageVersion=$VERSION -c $BUILD_CONFIGURATION --no-build -v m' 
  $cmd = 'dotnet nuget push "$PROJECT_DIR\src\$proj\bin\$BUILD_CONFIGURATION\$proj.$VERSION.nupkg" -s "$NUGET_SERVER"'
  if($NUGET_APIKEY) { $cmd = $cmd + " -k $NUGET_APIKEY" }
  Exec-Cmd "PUSH $proj" $cmd
} 

Nuget-PP 'GherkinSpec.Core'
Nuget-PP 'GherkinSpec.MsTest'
Nuget-PP 'GherkinSpec.NUnit'
Nuget-PP 'GherkinSpec.XUnit'

Print 'DONE'
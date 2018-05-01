
$PROJECT_DIR = if ($env:PROJECT_DIR) { $env:PROJECT_DIR } else { $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath('.\') }

. $PROJECT_DIR\scripts\_include.ps1

Exec-Cmd 'GIT VERSION INSTALL' 'choco install GitVersion.Portable'

Exec-Cmd 'GIT VERSION' 'GitVersion "$PROJECT_DIR" -updateassemblyinfo'

Exec-Cmd 'CLEAN' 'dotnet clean "$PROJECT_DIR\src\GherkinSpec.sln" -v m'

Exec-Cmd 'BUILD' 'dotnet build "$PROJECT_DIR\src\GherkinSpec.sln" -c $BUILD_CONFIGURATION -v m'

#Exec-Cmd 'TESTS' 'dotnet test "$PROJECT_DIR\src\GherkinSpec.sln" -c $BUILD_CONFIGURATION -v m' #This crap returns 1 because of being unable to understand NUnit tests even though it uses test adapter

Exec-Cmd 'TESTS' 'dotnet vstest "$PROJECT_DIR\src\GherkinSpec.Tests\bin\$BUILD_CONFIGURATION\netcoreapp2.0\GherkinSpec.Tests.dll" /TestAdapterPath:"$PROJECT_DIR\src\GherkinSpec.Tests\bin\$BUILD_CONFIGURATION\netcoreapp2.0" --Parallel'

Print 'DONE'
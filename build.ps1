
$PROJECT_DIR = $env:PROJECT_DIR
$BUILD_LOGGER = $env:BUILD_LOGGER

if (-not $PROJECT_DIR) 
{ 
  $PROJECT_DIR = $PSScriptRoot 
}

$build = 'msbuild "$PROJECT_DIR\src\GherkinSpec.sln" /verbosity:minimal'

if ($BUILD_LOGGER) 
{ 
  $build = $build + ' /logger:' + '"$BUILD_LOGGER"'
} 

iex $build
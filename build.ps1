$ErrorActionPreference = 'Stop'

$solutionName = 'Dhgms.NetContrib'
$solutionPath = 'src\\' + $solutionName + '.sln'
$testProject = 'src\\' + $solutionName + '.UnitTests\\' + $solutionName + '.UnitTests.csproj'

function CreateDirectoryIfItDoesNotExist([String] $DirectoryToCreate)
{
	if (-not (Test-Path -LiteralPath $DirectoryToCreate))
	{
		try
		{
			New-Item -Path $DirectoryToCreate -ItemType Directory -ErrorAction Stop | Out-Null #-Force
		}
		catch
		{
			Write-Error -Message "Unable to create directory '$DirectoryToCreate'. Error was: $_" -ErrorAction Stop
		}
	}
}

dotnet tool install --global dotMorten.OmdGenerator
dotnet tool install --global ConfigValidate
dotnet tool install --global dotnet-outdated
dotnet tool install --global snitch
dotnet tool install --global dotnet-sonarscanner
dotnet restore $solutionPath /bl:artifacts\\binlog\\restore.binlog

$runSonar = $false;

if ($runSonar)
{
	dotnet sonarscanner begin /k:"project-key"
}

dotnet build $solutionPath --configuration Release --no-restore /bl:artifacts\\binlog\\build.binlog

if ($runSonar)
{
	dotnet sonarscanner end
}

dotnet test $testProject --configuration Release --no-build --nologo --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover /bl:artifacts\\binlog\\test.binlog

CreateDirectoryIfItDoesNotExist('.\artifacts\nupkg');
dotnet pack $solutionPath --configuration Release --no-build /bl:artifacts\\binlog\\test.binlog /p:PackageOutputPath=..\artifacts\nupkg

CreateDirectoryIfItDoesNotExist('.\artifacts\outdated');
dotnet outdated -o artifacts\outdated\outdated.json src

CreateDirectoryIfItDoesNotExist('.\artifacts\snitch');
snitch src --strict > artifacts\snitch\snitch.txt

CreateDirectoryIfItDoesNotExist('.\artifacts\omd');
generateomd.exe /source=src /output=artifacts\omd\index.htm /format=html

CreateDirectoryIfItDoesNotExist('.\artifacts\docfx');
Remove-Item 'artifacts\docfx\*.*' -Recurse
xcopy src\docfx_project\_site artifacts\docfx /E /I /Y


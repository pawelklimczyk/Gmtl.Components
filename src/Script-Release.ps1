param (
	[switch]$patch,
	[switch]$minor,
	[switch]$major
)

Write-Host "This scripts increment csproj file version and publish nuget package"

$ErrorActionPreference = "Stop"

$mainProjFile = Resolve-Path ".\Gmtl.Components\Gmtl.Components.csproj"
$xml = [Xml] (Get-Content $mainProjFile)

#$currentVersion = [Version]($xml.Project.PropertyGroup | ? Condition -Like '' | select -ExpandProperty Version)

$currentVersion = New-Object System.Version(([System.String]$xml.Project.PropertyGroup.Version).Split('.'))

# if(git status --porcelain |Where {$_ -match '^\?\?'}){
	# throw 'Untracked files exist. Make sure your repository is clean before releasing.'
# }
# elseif(git status --porcelain |Where {$_ -notmatch '^\?\?'}) {
	# throw 'Uncommitted changes exist. Make sure to commit before releasing.'
# }

Write-Host "Previous version: $($currentVersion)"

if ($major) {
    $newVersion = New-Object System.Version(($currentVersion.Major + 1), 0, 0, 0)
} elseif ($minor) {
    $newVersion = New-Object System.Version($currentVersion.Major, ($currentVersion.Minor + 1), 0, 0)
} else {
    $newVersion = New-Object System.Version($currentVersion.Major, $currentVersion.Minor, ($currentVersion.Build + 1), 0) # *patch
}

$confirmation = Read-Host "Do you want to add release tag (v$($newVersion)) and push to remote server? [y/N]"
if ($confirmation -ne 'y') { 
    Write-Host "Exiting..."
    exit
}

Write-Host "New version: $($newVersion)"

Get-ChildItem -Path .\ -Filter *.csproj -Recurse -File -Name| ForEach-Object {
    try
    {
        #Write-Host "Checking file ${projFile}"
        $projFile = Resolve-Path $_
        $xml = [Xml] (Get-Content $projFile)
        $nodeFound = $FALSE
        $xml.Project.PropertyGroup.GetElementsByTagName("Version") | ForEach-Object {
            $_."#text" = "$($newVersion)"
            $nodeFound = $TRUE
        }

        if($nodeFound) {
            $xml.Save($projFile)
            git add $_
            Write-Host "Updating ${projFile}" -ForegroundColor Yellow
        }
    }
    catch {
        Write-Host "Error in file ${projFile}" -ForegroundColor Red
    }
}

dotnet clean -c Release
dotnet build -c Release -o publish

#dotnet pack .\Gmtl.Components\Gmtl.Components.csproj -c Release -o publish
if ($LASTEXITCODE -ne 0) { throw "Exit code is $LASTEXITCODE" }
#dotnet pack .\Gmtl.Components.Web\Gmtl.Components.Web.csproj -c Release -o publish
#if ($LASTEXITCODE -ne 0) { throw "Exit code is $LASTEXITCODE" }

dotnet nuget push .\publish\Gmtl.Components.$($newVersion.Major).$($newVersion.Minor).$($newVersion.Build).nupkg -k 12345 -s http://192.168.140.180:10101
if ($LASTEXITCODE -ne 0) { throw "Exit code is $LASTEXITCODE" }

dotnet nuget push .\publish\Gmtl.Components.Web.$($newVersion.Major).$($newVersion.Minor).$($newVersion.Build).nupkg -k 12345 -s http://192.168.140.180:10101
if ($LASTEXITCODE -ne 0) { throw "Exit code is $LASTEXITCODE" }

git commit -am "Release v$($newVersion)"
git tag v$($newVersion)
git push origin main --tags
git push

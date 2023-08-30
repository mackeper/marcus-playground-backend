function Clear-OutputDirectory([string] $outputDirectory) {
    if (Test-Path $outputDirectory) {
        Remove-Item -Path $outputDirectory\* -Force -Recurse
    }
}

function Publish-Projects([string[]] $projectPaths, [string] $outputDirectory) {
    foreach ($projectPath in $projectPaths) {
        $projectName = (Split-Path $projectPath -Leaf)
        $outputPath = Join-Path $outputDirectory $projectName
        
        Write-Host ("Publishing project {0}" -f $projectName) -ForegroundColor Yellow
        dotnet publish $projectPath -c Release -r linux-x64 --self-contained -o $outputPath /p:DebugType=None /p:DebugSymbols=false
        Write-Host ("Project {0} published to {1}" -f $projectName, $outputPath) -ForegroundColor Green
    }
}

function Main {
    $outputDirectory = "./out/"
    $projectPaths = @(
        ".\src\Dev\Dev\",
        ".\src\Blog\BlogService\"
    )

    Write-Host "Clearing output directory..." -ForegroundColor Blue
    Clear-OutputDirectory -OutputDirectory $outputDirectory
    Write-Host "Publishing projects..." -ForegroundColor Blue
    Publish-Projects -projectPaths $projectPaths -outputDirectory $outputDirectory
    Write-Host "Script execution completed." -ForegroundColor Green
}

Main

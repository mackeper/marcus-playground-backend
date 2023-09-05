<#
    This script automates the process of preparing files for use as a Docker volume by copying a specified list of files from a source directory to a destination directory.
    The destination directory is cleared before copying to ensure a clean state.

    Instructions:
    1. Modify the $sourceDir and $destinationDir variables to set the source and destination directories accordingly.
    2. Update the $fileList array with the list of files and directories to be copied from the source directory to the destination directory.
    3. Run the script using a PowerShell environment.
#>

function Copy-FilesToDirectory([string]$sourceDirectory, [string]$destinationDirectory, [string[]]$fileList) {
    foreach ($file in $fileList) {
        $sourcePath = Join-Path $sourceDirectory $file
        $destination = Join-Path $destinationDirectory $file
        Copy-Item -Path $sourcePath -Destination $destination -Recurse -Force
    }
}

function Clear-Directory([string]$directory) {
    Remove-Item -Path $directory -Recurse -Force
}

function New-Directory([string]$directory) {
    if (-not (Test-Path -Path $directory)) {
        New-Item -Path $directory -ItemType Directory
    }
}

function CheckAndCreateDockerImage ([string]$imageName, [string]$imageTag, [string]$dockerfilePath = ".") {
    if (docker images "${imageName}:${imageTag}" -q) {
        return;
    }

    Write-Host "Docker image ${imageName}:${imageTag} does not exist. Building..."
}
function StartDockerContainerIfNotRunning([string]$containerName, [string]$imageName, [string]$imageTag) {
    $containerStatus = docker ps -a --filter "name=$containerName" --format "{{.Status}}"

    if ($containerStatus -like "Up*") {
        Write-Host "Container $containerName is already running."
    } else {
        Write-Host "Container $containerName is not running. Starting..."
        docker run --name $containerName -v "$(Get-Location)\docker-volume:/docker-volume" -dit "${imageName}:${imageTag}"
    }
}

function RunStackBuildInDockerContainer([string]$containerName, [string]$workDir) {

    docker exec --workdir $workDir $containerName /bin/bash "-c" "stack build"
}

function Main {
    # Define source and destination directories
    $sourceDir = "."
    $destinationDir = ".\docker-volume"
    $outDir = ".\out"
    $imageName = "debian11-haskell"
    $imageTag = "v1.0"
    $containerName = "debian11-haskell"

    # Define the list of files to copy, relative to the source directory
    $fileList = @(
        "package.yaml",
        "helloworld.cabal",
        "stack.yaml",
        "stack.yaml.lock",
        "src",
        "app",
        "test",
        "Setup.hs",
        "LICENSE",
        "README.md",
        "CHANGELOG.md"
    )

    Clear-Directory -directory $destinationDir
    New-Directory -directory $destinationDir
    Copy-FilesToDirectory -sourceDirectory $sourceDir -destinationDirectory $destinationDir -fileList $fileList
    CheckAndCreateDockerImage -imageName $imageName -imageTag $imageTag -dockerfilePath $destinationDir
    StartDockerContainerIfNotRunning -containerName $containerName -imageName $imageName -imageTag $imageTag
    RunStackBuildInDockerContainer -containerName $containerName -workDir "/docker-volume"
    New-Directory -directory $outDir
}

Main

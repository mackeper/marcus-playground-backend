<#
    This script automates the process of preparing files for use as a Docker volume by copying a specified list of files from a source directory to a destination directory.
    The destination directory is cleared before copying to ensure a clean state.

    Instructions:
    1. Modify the $sourceDir and $destinationDir variables to set the source and destination directories accordingly.
    2. Update the $fileList array with the list of files and directories to be copied from the source directory to the destination directory.
    3. Run the script using a PowerShell environment.
#>

function Copy-FilesToDirectory {
    param (
        [string]$sourceDirectory,
        [string]$destinationDirectory,
        [string[]]$fileList
    )

    foreach ($file in $fileList) {
        $sourcePath = Join-Path $sourceDirectory $file
        Copy-Item -Path $sourcePath -Destination $destinationDirectory -Recurse -Force
    }
}

function Clear-Directory { param ([string]$directory)
    Remove-Item -Path $directory\* -Recurse -Force
}

function New-Directory { param ([string]$directory)
    if (-not (Test-Path -Path $directory)) {
        New-Item -Path $directory -ItemType Directory
    }
}

function Main {
    # Define source and destination directories
    $sourceDir = "."
    $destinationDir = ".\docker-volume"

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

    New-Directory -directory $destinationDir
    Clear-Directory -directory $destinationDir
    Copy-FilesToDirectory -sourceDirectory $sourceDir -destinationDirectory $destinationDir -fileList $fileList
}

Main

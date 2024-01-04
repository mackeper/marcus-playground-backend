$Path = Get-Item '.\src'
$Timeout = 1000



try
{
    Write-Host "FileSystemWatcher is monitoring $Path"
  
    $watcher = New-Object System.IO.FileSystemWatcher $Path
    $watcher.IncludeSubdirectories = $true

    Write-Host "Job started." 
    $job = Start-Job -ScriptBlock { cargo run }
  
    do
    {
        $result = $watcher.WaitForChanged("All", $Timeout)
        if ($result.TimedOut)
        {
            continue 
        }

        Write-Host 'Change detected:'
        Write-Host -ForegroundColor DarkYellow "$($result.ChangeType) $($result.Name)"

        if ($null -ne $job)
        { 
            $job.StopJob()
        }
        $job = Start-Job -ScriptBlock { cargo run }
    
    } while ($true)
} finally
{
    $watcher.Dispose()
    Write-Host 'FileSystemWatcher removed.'

    $job.StopJob()
    Write-Host 'Cargo process stopped.'
}

















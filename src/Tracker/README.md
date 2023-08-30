# Tracker

## Dev Setup

`https://www.haskell.org/ghcup/`  

`Set-ExecutionPolicy Bypass -Scope Process -Force;[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; try { Invoke-Command -ScriptBlock ([ScriptBlock]::Create((Invoke-WebRequest https://www.haskell.org/ghcup/sh/bootstrap-haskell.ps1 -UseBasicParsing))) -ArgumentList $true } catch { Write-Error $_ }`
### Stack
**Windows "Path" variable must be uppercase "PATH"**


`https://docs.haskellstack.org/en/stable/install_and_upgrade/`

`stack new helloworld new-template`  
`stack build`  
`stack exec helloworld-exe`  
`stack test`  

### VS code env
#### Extensions
##### Haskell Syntax Highlighting
##### haskell-linter
`stack install hlint`  
Add install path to env. Something like this on windows: `C:...\AppData\Roaming\local\bin`.



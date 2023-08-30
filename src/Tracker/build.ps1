New-Item -ItemType Directory -Path .\out -ErrorAction 'SilentlyContinue'
ghc -o .\out\Main -odir out Main.hs

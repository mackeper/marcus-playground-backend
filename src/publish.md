# Publish

## Linux

```
dotnet publish -c Release -r linux-x64 -o ./out/ /p:DebugType=None /p:DebugSymbols=false /p:PublishSingleFile=true
dotnet ef migrations bundle --self-contained -r linux-x64
Move-Item -Force efbundle 'bin\Release\net7.0\linux-x64\publish\'
scp -r C:\Users\macke\source\repos\JustABackend\src\Blog\BlogService\bin\Release\net7.0\linux-x64\publish\* gcp:~/Services/Blog/
```

# Just a backend
![haskell workflow](https://github.com/mackeper/marcus-playground-backend/actions/workflows/haskell.yml/badge.svg)
![C# workflow](https://github.com/mackeper/marcus-playground-backend/actions/workflows/dotnet.yml/badge.svg)

A collection of backend services used for my website [RealMoneyCompany](https://realmoneycompany.com/).


## Backend services
### Blog
`C# | /src/Blog | ASP.NET, .NET 7, EntityFramework`

Fetch and create blog entries.

### BoardGames
`Haskell | /src/BoardGames | Servant, Sqlite3-simple`

Serve the different board games on the site.

### Dev
`C# | /src/dev | ASP.NET, .NET 7`

Mostly different dev tools. E.g. generate a guid.

### Tracker
`Haskell | /src/Tracker | Servant, Sqlite3-simple`

Tracking visits on the website.
## Develop
### init user secrets
`dotnet user-secrets init`  
`dotnet user-secrets set "ConnectionStrings:default" "This is from user secrets"`  

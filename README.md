# Marcus Playground Backend :minidisc:

<div align="center">

![haskell workflow](https://github.com/mackeper/marcus-playground-backend/actions/workflows/haskell.yml/badge.svg)
![C# workflow](https://github.com/mackeper/marcus-playground-backend/actions/workflows/dotnet.yml/badge.svg)

A collection of backend services used for my website [RealMoneyCompany](https://realmoneycompany.com/).

[Development](#development-sparkles) •
[Roadmap](#roadmap-world_map) •
[FAQ](#faq-question) •
[Support](#support-love_letter)  

</div>

## Backend services

### [Blog](./src/Blog/README.md)

`C# | /src/Blog | ASP.NET, .NET 7, EntityFramework`

Fetch and create blog entries.

### [BoardGames](./src/BoardGames/README.md)

`Haskell | /src/BoardGames | Servant, Sqlite3-simple`

Serve the different board games on the site.

### [Dev](./src/Dev/README.md)

`C# | /src/dev | ASP.NET, .NET 7`

Mostly different dev tools. E.g. generate a guid.

### [Tracker](./src/Tracker/README.md)

`Haskell | /src/Tracker | Servant, Sqlite3-simple`

Tracking visits on the website.

## Development :sparkles:

### Prerequisites :heavy_check_mark:

- dotnet 7
  - Windows: [Download](https://dotnet.microsoft.com/download/dotnet/7.0)
- haskell stack
  - Windows: [GHCup](https://www.haskell.org/ghcup/)

### init user secrets

`dotnet user-secrets init`  
`dotnet user-secrets set "ConnectionStrings:default" "This is from user secrets"`  

## Roadmap :world_map:

- [ ] Add more board games :game_die:
- [ ] Add more dev tools :hammer_and_wrench:

## FAQ :question:

- Q: Why is the code so bad?
  - A:...

## Support :love_letter:

Submit an [issue!](https://github.com/mackeper/marcus-playground-backend/issues/new?assignees=&labels=question&projects=&template=question.yaml&title=%5BQUESTION%5D+%3Ctitle%3E)

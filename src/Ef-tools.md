# Install the tool
`dotnet tool install --global dotnet-ef`
`dotnet tool update --global dotnet-ef`

# Create inital migration
`dotnet ef migrations add InitialCreate`

# Apply mirgations
`dotnet ef database update`

## In production (Linux)
`dotnet ef migrations bundle --self-contained -r linux-x64`

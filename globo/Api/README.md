# Building an End-to-end SPA Using ASP.NET Core Web API and React
https://github.com/RolandGuijt/ps-globomantics-webapi-react

# Requirements
React 18
ASP.NET Core 8
Install .NET SDK (not just runtime)
Visual Studio Code
Node Latest LTS (Needed for package manager called npm)

# Install Tools
dotnet tool install --global dotnet-ef (entity framework core tools to access db)

# Web API
dotnet new webapi -minimal
dotnet build
dotnet run

dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.0
## For working with migrations
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0

## create migration with name initial
dotnet ef migrations add initial
dotnet ef database update

## minivalidation (Needed since mini api does not have validation)
dotnet add package minivalidation

## Entity Framework commands to add bids and seed data
dotnet ef migrations add bids
dotnet ef database update

# Notes
Swagger writes documenation for your API.  Implements standard called Open API. 
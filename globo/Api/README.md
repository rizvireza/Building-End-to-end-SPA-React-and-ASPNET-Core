# Building an End-to-end SPA Using ASP.NET Core Web API and React
https://github.com/RolandGuijt/ps-globomantics-webapi-react

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


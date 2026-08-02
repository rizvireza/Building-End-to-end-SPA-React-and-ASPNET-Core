# Requirements
React 18
ASP.NET Core 8
Install .NET SDK (not just runtime)
Visual Studio Code
Node Latest LTS (Needed for package manager called npm)

# Vite (mini web server)
npm create vite@5
1. select React framework
2. select typescript + SWC  (SWC tool used to convert typescript into javascript faster than Babel which is default)

# UI
Vite - provides starting templates for number of application types.
SWC - tool used to transpile tsx to javascript. Default is Babel but swift is much faster.  
npm create vite@5 (Use React and Typescript + SWC)
cd ReactWeb
npm install
npm run dev
(public folder has images etc. everything in src will be processed by vite)
npm install bootstrap
add import "bootstrap/dist/css/bootstrap.min.css" to main.tsx

npm install axios@1 @tanstack/react-query@5

npm install react-router-dom@6

# Folders
public - images etc
src - will be processed by vite

# Web API
dotnet tool install --global
dotnet new webapi -minimal
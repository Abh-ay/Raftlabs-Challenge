# RaftLabs-Challenge

# For Console App find below dependecies
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Microsoft.Extensions.Http
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json

# Add reference of Class Library project which encapsulate the process
dotnet add reference ../UserService/UserService.csproj

#For UserService Find below dependecies
dotnet add package Serilog.Settings.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json

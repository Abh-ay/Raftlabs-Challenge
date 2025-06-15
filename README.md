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


#To get Walkthrough of Working Demo
Build the Console App and press any key to start the process.
You will see below 2 response
1. GetUserById() where find user base in their Id and get No result found if no any use found with respoect to that Id
2. GetAllUser() where collect all user details from all mentioned pages and return list of users.

#Refer below screenshot of Console App
![Alt text](assets/ConsoleImage.jpg)

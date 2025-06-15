using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserService.Services; // ✅ Your namespace


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.AddHttpClient<UserService.Services.UserService>(client =>
{
    client.BaseAddress = new Uri(config["Configuration:BaseUrl"]);
    client.DefaultRequestHeaders.Add("x-api-key", config["Configuration:ApiKey"]);

});

services.AddLogging(configure => configure.AddConsole());
var provider = services.BuildServiceProvider();
var service = provider.GetRequiredService<UserService.Services.UserService>();

//Get User By id
Console.WriteLine("******Calling Get User by Id fucntion*****");
var user = await service.GetUserByIdAsync(2);
Console.WriteLine($"{user.Id} - {user.First_Name} {user.Last_Name}");
Console.WriteLine("******Calling Get User by Id fucntion Completed*****");


//Get User List
Console.WriteLine("******Calling Get All user fucntion*****");
var userList = await service.GetAllUsersAsync();
foreach (var i in userList)
    Console.WriteLine($"{i.Id} - {i.First_Name} {i.Last_Name}");

Console.WriteLine("******Calling Get All user fucntion Completed*****");
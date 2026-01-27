// See https://aka.ms/new-console-template for more information
using MongoDotnetORM.Sample.Models;
using src;

async Task RunTestAsync()
{
    string connectionString = "mongodb://localhost:27017";
    string databaseName = "YourDatabaseName";

    var dataSrc = new DataSource(connectionString, databaseName);
    var userRepo = dataSrc.GetRepository<User>();

    var sonam = new User { Name = "Sonam", Email = "sjjamtsho@gmail.com", Password = "password" };

    Console.WriteLine("The user below is about to be inserted:");
    Console.WriteLine($"Name: {sonam.Name}, Email: {sonam.Email}, Password: {sonam.Password}");

 

}


await RunTestAsync();




// See https://aka.ms/new-console-template for more information
using MongoDotnetORM.Sample;
using src;

async Task RunTestAsync()
{
    string connectionString = "mongodb://localhost:27017";
    string databaseName = "YourDatabaseName";

    var dataSrc = new DataSource(connectionString, databaseName);
    var userRepo = dataSrc.GetRepository<User>();

    // CREATE
    var newUser = await userRepo.InsertAsync(new User
    {
        Username = "john_doe",
        Email = "john@example.com",
        Age = 30,
        Tags = new List<string> { "developer", "admin" }
    });

    Console.WriteLine($"Created user with ID: {newUser.Id}");
    
    // READ
    var user = await userRepo.FindByIdAsync(newUser.Id);
    var allUsers = await userRepo.FindAllAsync();
    var filteredUsers = await userRepo.FindAsync(u => u.Age > 25);
    var oneUser = await userRepo.FindOneAsync(u => u.Email == "john@example.com");

    // UPDATE
    user.Age = 31;
    await userRepo.UpdateAsync(user.Id, user);

    // DELETE
    await userRepo.DeleteAsync(user.Id);

    // Using LINQ queries
    var query = userRepo.AsQueryable()
        .Where(u => u.Age > 25)
        .OrderBy(u => u.Username)
        .Take(10);

    var queryTwo = userRepo.AsQueryable().Where(d => d.Age > 0);

    var results = query.ToList();
    var results2 = queryTwo.ToList();

    Console.WriteLine("Printing users greater than 25 whose isername is john_doe");
   foreach( var item in results )
        Console.WriteLine(item);

    Console.WriteLine("Printing users greater than 0");
    foreach (var item in results2)
        Console.WriteLine(item);

    // Count
    var count = await userRepo.CountAsync(u => u.Age > 25);

    Console.WriteLine($"Found {count} users over 25");

}

await RunTestAsync();




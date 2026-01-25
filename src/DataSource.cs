using MongoDB.Driver;

namespace MongoDotnetORM
{
    public class DataSource
    {
        public IMongoDatabase database;

        string connectionString = "mongodb://localhost:27017";
        string databaseName = "YourDatabaseName";

        public DataSource()
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public Repository<T> GetRepository<T>() where T : BaseEntity
        {
            var repo = new Repository<T>(database);
            return repo;
        }
    }
}

using MongoDB.Driver;

namespace src
{
    public class DataSource
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public IMongoDatabase database;

        public DataSource(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            if(_repositories.ContainsKey(type))
            {
                return (IRepository<T>)_repositories[type];
            }

            var collection = database.GetCollection<T>(typeof(T).ToString() + 's');
            var repo = new Repository<T>(collection);
            _repositories[type] = repo;
            return repo;
        }
    }
}

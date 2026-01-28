using MongoDB.Driver;
using System.Reflection;

namespace src
{
    public class DataSource
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly IMongoDatabase _database;

        public DataSource(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            if(_repositories.ContainsKey(type))
            {
                return (IRepository<T>)_repositories[type];
            }

            var collectionName = GetCollectionName<T>();
            var collection = _database.GetCollection<T>(collectionName);
            var repo = new Repository<T>(collection);
            _repositories[type] = repo;
            return repo;
        }

        private string GetCollectionName<T>()
        {
            var type = typeof(T);
            var entityAttr = type.GetCustomAttribute<EntityAttribute>();

            if (entityAttr != null && !string.IsNullOrEmpty(entityAttr.CollectionName))
            {
                return entityAttr.CollectionName;
            }

            return type.Name.ToLower() + "s";
        }
    }
}

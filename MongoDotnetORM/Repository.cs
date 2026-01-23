using MongoDB.Driver;

namespace MongoDotnetORM
{
    public class Repository<T>: IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public Repository(IMongoDatabase db)
        {
            _collection = db.GetCollection<T>(typeof(T).Name + "s");
        }
        public async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await _collection.Find(_=> true).ToListAsync();
        }

        public async Task<T> FindByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}

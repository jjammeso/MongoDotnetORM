using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoDotnetORM
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }
        public async Task<T> InsertAsync(T entity)
        {
            SetTimestamps(entity, isNew: true);
            await _collection.InsertOneAsync(entity);
            return entity;
        }
        Task<List<T>> InsertManyAsync(IEnumerable<T> entities);
        Task<T> FindByIdAsync(string id);
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter);

        Task<T> UpdateAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);

        Task<long> DeleteManyAsync(Expression<Func<T, bool>> filter);

        Task<long> CountAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> AsQueryable();
    }
}

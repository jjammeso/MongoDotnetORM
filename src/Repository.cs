using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;

namespace src
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

        public async Task<List<T>> InsertManyAsync(IEnumerable<T> entities)
        {
            var entitiesList = entities.ToList();

            foreach(var entity in entitiesList)
            {

                SetTimestamps(entity, isNew: true);
            }
            await _collection.InsertManyAsync(entitiesList);

            return entitiesList;
        }

        public async Task<T> FindByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<T>> FindAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            SetTimestamps(entity, isNew: false);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            await _collection.ReplaceOneAsync(filter, entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<long> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _collection.DeleteManyAsync(filter);
            return result.DeletedCount;
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
                return await _collection.CountDocumentsAsync(_ => true);
            return await _collection.CountDocumentsAsync(filter);
        }
        public IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        private void SetTimestamps(T entity, bool isNew)
        {
            var now = DateTime.UtcNow;

            if(entity is BaseEntity entityBase)
            {
                if(isNew)
                    entityBase.CreatedAt = now;
                entityBase.UpdatedAt = now;
            }
            else
            {
                var props = typeof(T).GetProperties();

                foreach (var item in props)
                {
                    if(isNew && item.GetCustomAttributes<CreatedDateAttribute>() != null)
                    {
                        item.SetValue(entity, now);
                    }

                    if (item.GetCustomAttribute<UpdatedDateAttribute>() != null)
                    {
                        item.SetValue(entity, now);
                    }
                }
            }
        }
    }
}

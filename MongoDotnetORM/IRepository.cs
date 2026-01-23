
using System.Linq.Expressions;

namespace MongoDotnetORM
{
    public interface IRepository<T> where T : class
    {
        Task<T> InsertAsync(T entity);
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

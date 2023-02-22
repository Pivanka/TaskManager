using System.Linq.Expressions;

namespace DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : DAL.Models.BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}

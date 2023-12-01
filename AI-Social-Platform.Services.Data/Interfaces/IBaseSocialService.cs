using System.Linq.Expressions;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        Task<TEntity> GetByIdAsync(Guid id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid entityId);
    }
}

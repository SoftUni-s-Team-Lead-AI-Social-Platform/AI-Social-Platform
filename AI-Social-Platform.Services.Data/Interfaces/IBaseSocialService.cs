using System.Linq.Expressions;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        Task<TEntity> GetByIdAsync(Guid id);
        Task UpdatePropertyAsync(TEntity entity, Expression<Func<TEntity, string>> propertyExpression,
            string newValue);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}

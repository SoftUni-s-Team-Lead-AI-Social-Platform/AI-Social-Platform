using System.Linq.Expressions;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService<TEntity,TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        Task<TDto> GetByIdAsync(Guid id);
        Task CreateAsync(TDto dto);
        Task UpdateAsync(TDto dto, Guid id);
        Task DeleteAsync(Guid id);
    }
}

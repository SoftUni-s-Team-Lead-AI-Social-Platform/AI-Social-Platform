using System.Linq.Expressions;
using System.Reflection;
using AI_Social_Platform.Data;
using AI_Social_Platform.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AI_Social_Platform.Services.Data
{
    public class BaseSocialService<TEntity> : IBaseSocialService<TEntity> 
        where TEntity : class 
    
    {
        private readonly ASPDbContext _dbContext;

        public BaseSocialService(ASPDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null!,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
                query = query.Where(filter);
                query = orderBy(query);

                return query;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity!;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePropertyAsync(TEntity entity,
            Expression<Func<TEntity, string>> propertyExpression,
            string newValue)
        {
            var propertyInfo = ((MemberExpression)propertyExpression.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Invalid property expression");
            }

            _dbContext.Entry(entity).Property(propertyInfo.Name).CurrentValue = newValue;
            _dbContext.Entry(entity).Property(propertyInfo.Name).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity!);
            await _dbContext.SaveChangesAsync();
        }
    }
}

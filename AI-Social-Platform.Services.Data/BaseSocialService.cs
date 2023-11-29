using System.Linq.Expressions;
using AI_Social_Platform.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AI_Social_Platform.Services.Data
{
    public class BaseSocialService<TEntity, TDto> : IBaseSocialService<TEntity,TDto> 
        where TEntity : class 
        where TDto : class
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public BaseSocialService(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TDto>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var entities = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public Task CreateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

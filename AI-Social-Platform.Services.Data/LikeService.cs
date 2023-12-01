using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;

namespace AI_Social_Platform.Services.Data
{
    public class LikeService : BaseSocialService<Like>
    {
        private readonly ASPDbContext _dbContext;
        public LikeService(ASPDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

}
}

namespace AI_Social_Platform.Services.Data
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.Data.Models.Topic;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TopicService : ITopicService
    {
        private readonly ASPDbContext dbContext;
        public TopicService(ASPDbContext dbContext)
        {
                this.dbContext = dbContext;
        }
        public async Task FollowTopicAsync(string userId, string topicId)
        {
            
        }
    }
}

using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AI_Social_Platform.Services.Data
{
    public class BaseSocialService : IBaseSocialService
    {
        private readonly ASPDbContext dbContext;

        public BaseSocialService(ASPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateNotificationAsync(Guid receivingUserId, Guid creatingUserId, NotificationType notificationType, Guid? returningId)
        {
            var user = await dbContext.Users.Where(u => u.Id == creatingUserId)
                .Select(u => u.FirstName + " " + u.LastName).FirstOrDefaultAsync();
            
            var notification = new Notification()
            {
                ReceivingUserId = receivingUserId,
                CreatingUserId = creatingUserId,
                NotificationType = notificationType
            };

            switch (notificationType)
            {
                case NotificationType.Comment: 
                    notification.Content = $"{user} commented your publication"; 
                    notification.RedirectUrl = $"/Publication/{returningId}";
                    break;
                case NotificationType.Like: 
                    notification.Content = $"{user} liked your publication";
                    notification.RedirectUrl = $"/Publication/{returningId}";
                    break;
                case NotificationType.Follow:
                    notification.Content = $"{user} followed you";
                    notification.RedirectUrl = $"/User/userDetails/{creatingUserId}";
                    break;
                case NotificationType.Share: 
                    notification.Content = $"{user} shared your publication";
                    notification.RedirectUrl = $"/Publication/{returningId}";
                    break;
            }

            await dbContext.Notifications.AddAsync(notification);
            await dbContext.SaveChangesAsync();
        }
    }
}

using AI_Social_Platform.Data.Models.Enums;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService
    {
        public Task CreateNotificationAsync(Guid receivingUserId, 
            Guid creatingUserId,
            NotificationType notificationType,
            Guid? returningId);
    }
}

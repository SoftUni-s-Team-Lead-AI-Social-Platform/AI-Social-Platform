using System.Collections;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AI_Social_Platform.Services.Data.Models.SocialFeature;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService
    {
        public Task CreateNotificationAsync(Guid receivingUserId, 
            Guid creatingUserId,
            NotificationType notificationType,
            Guid? returningId);

        public Task<IEnumerable<NotificationDto>> GetLatestNotificationsAsync();
        public Task<IEnumerable> SearchAsync(string type, string query);

        //Comments
        public Task<IEnumerable<CommentDto>> GetCommentsOnPublicationAsync(Guid publicationId);
        public Task CreateCommentAsync(CommentFormDto dto);
        public Task UpdateCommentAsync(CommentFormDto dto, Guid id);
        public Task DeleteCommentAsync(Guid id);

        //Likes
        public Task<IEnumerable<LikeDto>> GetLikesOnPublicationAsync(Guid publicationId);
        public Task CreateLikesOnPublicationAsync(Guid publicationId);
        public Task DeleteLikeOnPublicationAsync(Guid likeId);

        //Shares
        public Task<IEnumerable<ShareDto>> GetSharesOnPublicationAsync(Guid publicationId);
        public Task CreateSharesOnPublicationAsync(Guid publicationId);
        public Task DeleteShareOnPublicationAsync(Guid shareId);

    }
}

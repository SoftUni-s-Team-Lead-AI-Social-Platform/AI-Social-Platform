﻿using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AI_Social_Platform.Services.Data.Models.UserDto;
using static AI_Social_Platform.Common.ExceptionMessages.PublicationExceptionMessages;

namespace AI_Social_Platform.Services.Data
{
    public class BaseSocialService : IBaseSocialService
    {
        private readonly ASPDbContext dbContext;
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;

        public BaseSocialService(ASPDbContext dbContext, IHttpContextAccessor accessor, IMapper mapper)
        {
            this.dbContext = dbContext;
            httpContext = accessor.HttpContext!;
            this.mapper = mapper;
        }

        public async Task CreateNotificationAsync(Guid receivingUserId, Guid creatingUserId, NotificationType notificationType, Guid? returningId)
        {
            var user = await dbContext.Users
                .Where(u => u.Id == creatingUserId)
                .Select(u => u.FirstName + " " + u.LastName)
                .FirstOrDefaultAsync();
            
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
                    notification.RedirectUrl = $"/posts/{returningId}";
                    break;
                case NotificationType.Like: 
                    notification.Content = $"{user} liked your publication";
                    notification.RedirectUrl = $"/posts/{returningId}";
                    break;
                case NotificationType.Follow:
                    notification.Content = $"{user} added you as a friend";
                    notification.RedirectUrl = $"/profile/{creatingUserId}";
                    break;
                case NotificationType.Share: 
                    notification.Content = $"{user} shared your publication";
                    notification.RedirectUrl = $"/posts/{returningId}";
                    break;
            }

            await dbContext.Notifications.AddAsync(notification);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationDto>> GetLatestNotificationsAsync()
        {
            int notificationListSize = 20;

            return await mapper.ProjectTo<NotificationDto>
            (dbContext.Notifications
                .AsQueryable()
                .Where(n => n.ReceivingUserId == GetUserId() && n.IsRead == false)
                .OrderByDescending(n => n.DateCreated)
                .Take(notificationListSize))
                .ToArrayAsync();
        }

        public async Task<int> GetNotificationsCountAsync()
        {
            return await dbContext.Notifications
                .AsQueryable()
                .Where(n => n.ReceivingUserId == GetUserId() && n.IsRead == false)
                .CountAsync();
        }

        public async Task ReadNotificationAsync(Guid notificationId)
        {
            var notification = await dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId);

            if (notification == null)
            {
                throw new NullReferenceException(NotificationNotFound);
            }

            notification.IsRead = true;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable> SearchAsync(string type, string query)
        {
            int take = 20;
            type = type.ToLower();

            var result = type switch
            {
                "user" => await SearchUsersAsync(query, take),
                "publication" => await SearchPublicationsAsync(query, take),
                "topic" => await SearchTopicsAsync(query, take),
                _ => throw new ArgumentException("Invalid search type"),
            };
            return result;
        }

        private async Task<IEnumerable> SearchUsersAsync(string query, int take)
        {
            var users = await mapper.ProjectTo<UserDto>
            (dbContext.Users
                           .AsQueryable()
                           .Where(u => u.FirstName.Contains(query) || u.LastName.Contains(query))
                           .Take(take))
                .ToArrayAsync();

            foreach (var user in users)
            {
                if (user.ProfilePictureData != null)
                {
                    user.ProfilePictureUrl = GetProfilePicturePath(user.Id);
                    user.ProfilePictureData = null;
                }
            }

            return users;
        }

        private async Task<IEnumerable> SearchPublicationsAsync(string query, int take)
        {
            var publications = await mapper.ProjectTo<PublicationDto>
                (dbContext.Publications
                .AsQueryable()
                .Where(p => p.Content.Contains(query))
                .OrderByDescending(p => p.LatestActivity)
            .Take(take))
                .ToListAsync();

           publications = ReturnListDtoWithImages(publications);

            return publications;
        }

        private async Task<IEnumerable> SearchTopicsAsync(string query, int take)
        {
            return 
                await mapper.ProjectTo<SearchTopicDto>(dbContext.Topics
                        .Include(t => t.Followers)
                        .Include(t => t.Publications)
                        .AsQueryable()
                        .Where(t => t.Title.Contains(query))
                        .OrderByDescending(t => t.Followers.Count)
                        .Take(take))
                        .ToArrayAsync();
        }

        //Comment
        public async Task<CommentDto> CreateCommentAsync(CommentFormDto dto)
        {
            var publication = await dbContext.Publications
                .FirstOrDefaultAsync(p => p.Id == dto.PublicationId);
            var userId = GetUserId();

            if (publication == null)
            {
                throw new NullReferenceException(PublicationNotFound);
            }

            var comment = mapper.Map<Comment>(dto);
            comment.UserId = userId;
            publication.LastCommented = DateTime.UtcNow;
            publication.LatestActivity = DateTime.UtcNow;

            if (publication.AuthorId != userId)
            {
                await CreateNotificationAsync(publication.AuthorId, userId, NotificationType.Comment, publication.Id);
            }
            
            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();

            var user = await dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProfilePictureData = u.ProfilePicture
                })
                .FirstOrDefaultAsync();

            if (user?.ProfilePictureData != null)
            {
                user.ProfilePictureUrl = GetProfilePicturePath(user.Id);
                user.ProfilePictureData = null;
            }

            var dtoReturn = mapper.Map<CommentDto>(comment);
            dtoReturn.User = user!;

            return dtoReturn;
        }

        public async Task<CommentDto> UpdateCommentAsync(CommentEditDto dto, Guid id)
        {
            var comment = await dbContext.Comments.FindAsync(id);
            var userId = GetUserId();

            if (comment == null)
            {
                throw new NullReferenceException(CommentNotFound);
            }

            if (comment.UserId != userId)
            {
                throw new AccessViolationException(NotAuthorizedToEditComment);
            }
            comment.Content = dto.Content;
            await dbContext.SaveChangesAsync();

            var commentDto = mapper.Map<CommentDto>(comment);
            commentDto.User = mapper.Map<UserDto>(await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId));

            if (commentDto.User.ProfilePictureData != null)
            {
                commentDto.User.ProfilePictureUrl = GetProfilePicturePath(commentDto.User.Id);
                commentDto.User.ProfilePictureData = null;
            }

            return commentDto;
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            var userId = GetUserId();

            if (comment == null)
            {
                throw new NullReferenceException(CommentNotFound);
            }

            if (comment.UserId != userId)
            {
                throw new AccessViolationException(NotAuthorizedToDeleteComment);
            }
            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IndexCommentDto> GetCommentsOnPublicationAsync(Guid publicationId, int pageNum)
        {
            int pageSize = 5;
            int skip = 5;
            skip += (pageNum - 1) * pageSize;
            int totalComments = await dbContext.Comments.Where(p => p.PublicationId == publicationId).CountAsync();
            int commentsLeft = totalComments - skip - pageSize < 0 ? 0 : totalComments - skip - pageSize;

            var comments = await dbContext.Comments
                .Where(c => c.PublicationId == publicationId)
                .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.DateCreated)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            foreach (var comment in comments)
            {
                if (comment.User.ProfilePictureData != null)
                {
                    comment.User.ProfilePictureUrl = GetProfilePicturePath(comment.User.Id);
                    comment.User.ProfilePictureData = null;
                }
            }

            return new IndexCommentDto()
            {
                Comments = comments,
                CurrentPage = pageNum,
                TotalComments = totalComments,
                TotalPages = (int)Math.Ceiling((double)totalComments / pageSize),
                CommentsLeft = commentsLeft
            };
        }

        //Like
        public async Task<LikeDto> CreateLikesOnPublicationAsync(Guid publicationId)
        {
            var publication = await dbContext.Publications
                .FirstOrDefaultAsync(p => p.Id == publicationId);
            var userId = GetUserId();

            if (publication == null)
            {
                throw new NullReferenceException(PublicationNotFound);
            }

            if (await dbContext.Likes.AnyAsync(l => l.PublicationId == publicationId && l.UserId == userId))
            {
                throw new InvalidOperationException(AlreadyLiked);
            }

            var like = new Like
            {
                PublicationId = publicationId,
                UserId = userId
            };

            if (publication.AuthorId != userId)
            {
                await CreateNotificationAsync(
                    publication.AuthorId,
                    userId,
                    NotificationType.Like,
                    publication.Id);
            }
            await dbContext.Likes.AddAsync(like);
            await dbContext.SaveChangesAsync();

            var likeDto = mapper.Map<LikeDto>(like);

            if (likeDto.User.ProfilePictureData != null)
            {
                likeDto.User.ProfilePictureUrl = GetProfilePicturePath(likeDto.User.Id);
                likeDto.User.ProfilePictureData = null;
            }

            return likeDto;
        }

        public async Task<LikeDto> DeleteLikeOnPublicationAsync(Guid likeId)
        {
            var like = await dbContext.Likes.FirstOrDefaultAsync(l => l.Id == likeId);
            var userId = GetUserId();

            if (like == null)
            {
                throw new NullReferenceException(LikeNotFound);
            }

            if (like.UserId != userId)
            {
                throw new AccessViolationException(NotAuthorizedToDeleteLike);
            }

            dbContext.Likes.Remove(like);
            await dbContext.SaveChangesAsync();

            var likeDto = mapper.Map<LikeDto>(like);

            if (likeDto.User.ProfilePictureData != null)
            {
                likeDto.User.ProfilePictureUrl = GetProfilePicturePath(likeDto.User.Id);
                likeDto.User.ProfilePictureData = null;
            }

            return mapper.Map<LikeDto>(like);
        }

        public async Task<IEnumerable<LikeDto>> GetLikesOnPublicationAsync(Guid publicationId)
        {
            var likesDto = await dbContext.Likes
                .Where(l => l.PublicationId == publicationId)
                .ProjectTo<LikeDto>(mapper.ConfigurationProvider)
                .OrderByDescending(l => l.DateCreated)
                .ToListAsync();

            foreach (var like in likesDto)
            {
                if (like.User.ProfilePictureData != null)
                {
                    like.User.ProfilePictureUrl = GetProfilePicturePath(like.User.Id);
                    like.User.ProfilePictureData = null;
                }
            }

            return likesDto;
        }

        //Share
        public async Task CreateSharesOnPublicationAsync(Guid publicationId)
        {
            var publication = await dbContext.Publications
                .FirstOrDefaultAsync(p => p.Id == publicationId);
            var userId = GetUserId();

            if (publication == null)
            {
                throw new NullReferenceException(PublicationNotFound);
            }

            var share = new Share
            {
                PublicationId = publicationId,
                UserId = userId
            };

            if (publication.AuthorId != userId)
            {
                await CreateNotificationAsync(
                    publication.AuthorId,
                    userId,
                    NotificationType.Share,
                    publicationId);
            }
            await dbContext.Shares.AddAsync(share);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteShareOnPublicationAsync(Guid shareId)
        {
            var share = await dbContext.Shares.FirstOrDefaultAsync(s => s.Id == shareId);
            var userId = GetUserId();

            if (share == null)
            {
                throw new NullReferenceException(ShareNotFound);
            }

            if (share.UserId != userId)
            {
                throw new AccessViolationException(NotAuthorizedToDeleteShare);
            }

            dbContext.Shares.Remove(share);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShareDto>> GetSharesOnPublicationAsync(Guid publicationId)
        {
            return await dbContext.Shares
                .Where(s => s.PublicationId == publicationId)
                .ProjectTo<ShareDto>(mapper.ConfigurationProvider)
                .OrderByDescending(s => s.DateCreated)
                .ToListAsync();
        }

        private Guid GetUserId()
        {
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            if (userId == null)
            {
                throw new NullReferenceException();
            }

            return Guid.Parse(userId);
        }

        private List<PublicationDto> ReturnListDtoWithImages(List<PublicationDto> publications)
        {
            publications.ForEach(p =>
            {
                if (p.Author.ProfilePictureData != null)
                {
                    p.Author.ProfilePictureUrl = GetProfilePicturePath(p.Author.Id);
                    p.Author.ProfilePictureData = null;
                }

                foreach (var comment in p.Comments)
                {
                    if (comment.User.ProfilePictureData != null)
                    {
                        comment.User.ProfilePictureUrl = GetProfilePicturePath(comment.User.Id);
                        comment.User.ProfilePictureData = null;
                    }
                }
            });

            return publications;
        }

        private string GetProfilePicturePath(Guid userId)
        {
            var request = httpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            var path = $"{baseUrl}/api/User/ProfilePicture/{userId}";
            return path;
        }
    }
}

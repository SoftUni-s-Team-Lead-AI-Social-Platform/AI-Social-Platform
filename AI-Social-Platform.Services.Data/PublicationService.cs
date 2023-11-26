using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Social_Platform.Services.Data;

public class PublicationService : IPublicationService
{
    private readonly ASPDbContext dbContext;
    private readonly HttpContext httpContext;

    public PublicationService(ASPDbContext dbContext, IHttpContextAccessor accessor)
    {
        this.dbContext = dbContext;
        this.httpContext = accessor.HttpContext!;
    }
    public async Task<IEnumerable<PublicationDto>> GetPublicationsAsync()
    {
        var publications = await
            dbContext.Publications
                .Select(p => new PublicationDto()
                {
                    Id = p.Id,
                    Content = p.Content,
                    DateCreated = p.DateCreated,
                    AuthorId = p.AuthorId
                })
                .OrderByDescending(p => p.DateCreated)
                .ToListAsync();

        return publications;
    }
    
    public async Task<PublicationDto> GetPublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        PublicationDto publicationDto = new()
        {
            Id = publication.Id,
            Content = publication.Content,
            DateCreated = publication.DateCreated,
            AuthorId = publication.AuthorId
        };

        return publicationDto;

    }
    
    public async Task CreatePublicationAsync(PublicationFormDto dto)
    {
        var userId = await GetUserId();
        Publication publication = new()
        {
           Content = dto.Content,
           AuthorId = userId
        };

       await dbContext.AddAsync(publication);
       await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdatePublicationAsync(PublicationFormDto dto, Guid id)
    {
        var publication = await dbContext.Publications.FirstOrDefaultAsync(p => p.Id == id);
        var userId = await GetUserId();

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        if (publication.AuthorId != userId)
        {
            throw new Exception("You are not the author of this publication");
        }

        publication.Content = dto.Content;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeletePublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            //.Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        var userId = await GetUserId();

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        if (publication.AuthorId != userId)
        {
            throw new Exception("You are not the author of this publication");
        }

        //dbContext.Comments.RemoveRange(publication.Comments);
        dbContext.Publications.Remove(publication);
        await dbContext.SaveChangesAsync();
    }

    //Comment
    public async Task CreateCommentAsync(CommentFormDto dto, Guid publicationId)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == publicationId);
        var userId = await GetUserId();

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        await dbContext.Comments.AddAsync(new Comment()
        {
            AuthorId = userId,
            Content = dto.Content,
            PublicationId = publicationId
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(CommentFormDto dto, Guid id)
    {
       var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
       var userId = await GetUserId();

        if (comment == null)
       {
           throw new Exception("Comment not found");
       }

       if (comment.AuthorId != userId)
       {
           throw new Exception("You are not the author of this comment");
       }

       comment.Content = dto.Content;
       await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        var userId = await GetUserId();

        if (comment == null)
        {
            throw new Exception("Comment not found");
        }

        if (comment.AuthorId != userId)
        {
            throw new Exception("You are not the author of this comment");
        }

        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsOnPublicationAsync(Guid publicationId)
    {
        var publicationComments = await dbContext.Comments
            .Where(c => c.PublicationId == publicationId)
            .Select(c => new CommentDto()
            {
                Id = c.Id,
                Content = c.Content,
                DateCreated = c.DateCreated,
                AuthorId = c.AuthorId,
                PublicationId = c.PublicationId
            })
            .OrderByDescending(c => c.DateCreated)
            .ToListAsync();

        return publicationComments;
    }

    private async Task<Guid> GetUserId()
    {
        var userEmail = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        var userId = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (userId == null)
        {
            throw new Exception("User not found");
        }

        return userId.Id;
    }
}
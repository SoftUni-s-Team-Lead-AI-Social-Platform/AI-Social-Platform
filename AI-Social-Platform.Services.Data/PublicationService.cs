using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using static AI_Social_Platform.Common.ExceptionMessages.PublicationExceptionMessages;

namespace AI_Social_Platform.Services.Data;

public class PublicationService : IPublicationService
{
    private readonly ASPDbContext dbContext;
    private readonly HttpContext httpContext;
    private readonly IMapper mapper;

    public PublicationService(ASPDbContext dbContext, IHttpContextAccessor accessor, IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
        httpContext = accessor.HttpContext!;
    }
    public async Task<IEnumerable<PublicationDto>> GetPublicationsAsync()
    {
       return await mapper.ProjectTo<PublicationDto>(dbContext.Publications.AsQueryable())
           .OrderByDescending(p => p.DateCreated).ToListAsync();
    }

    public async Task<PublicationDto> GetPublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }
        return mapper.Map<PublicationDto>(publication);
    }
    
    public async Task CreatePublicationAsync(PublicationFormDto dto)
    {
        var userId = GetUserId();
        var publication = mapper.Map<Publication>(dto);
        publication.AuthorId = userId;

       await dbContext.AddAsync(publication);
       await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdatePublicationAsync(PublicationFormDto dto, Guid id)
    {
        var publication = await dbContext.Publications.FirstOrDefaultAsync(p => p.Id == id);
        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }

        if (publication.AuthorId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToEditPublication);
        }

        publication.Content = dto.Content;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeletePublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications.FirstOrDefaultAsync(p => p.Id == id);

        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }

        if (publication.AuthorId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToDeletePublication);
        }

        dbContext.Publications.Remove(publication);
        await dbContext.SaveChangesAsync();
    }

    //Comment
    public async Task CreateCommentAsync(CommentFormDto dto)
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

        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(CommentFormDto dto, Guid id)
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

    public async Task<IEnumerable<CommentDto>> GetCommentsOnPublicationAsync(Guid publicationId)
    {
        return await dbContext.Comments
            .Where(c => c.PublicationId == publicationId)
            .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
            .OrderByDescending(c => c.DateCreated)
            .ToListAsync();
    }

    private Guid GetUserId()
    {
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

        if (userId == null)
        {
            throw new NullReferenceException(PublicationAuthorNotFound);
        }

        return Guid.Parse(userId);
    }
}
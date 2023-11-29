using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AutoMapper;


namespace AI_Social_Platform.Services.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Comment, CommentDto>(); 
            CreateMap<Like, LikeDto>();
            CreateMap<Share, ShareDto>();
        }
    }
}

namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class CommentDto : BaseSocialDto
    { 
        public string Content { get; set; } = null!;

        public UserDto.UserDto User { get; set; } = null!;
    }
}

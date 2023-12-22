namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class BaseSocialDto
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid PublicationId { get; set; }

        public UserDto.UserDto User { get; set; } = null!;
    }
}

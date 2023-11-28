namespace AI_Social_Platform.Data.Configurations
{
    using AI_Social_Platform.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MediaEntityConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder
                .HasOne(m => m.Publication)
                .WithMany(p => p.MediaFiles)
                .HasForeignKey(m => m.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

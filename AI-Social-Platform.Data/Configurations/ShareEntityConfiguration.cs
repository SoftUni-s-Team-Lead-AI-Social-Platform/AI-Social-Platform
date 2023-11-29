using AI_Social_Platform.Data.Models.Publication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Social_Platform.Data.Configurations
{
    public class ShareEntityConfiguration : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder
                .HasOne(x => x.Publication)
                .WithMany(x => x.Shares)
                .HasForeignKey(x => x.PublicationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Shares)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class CommentConfigurations : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Subject)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();
        
        
        builder.Property(x => x.Body)
            .HasMaxLength(LengthConstants.DescriptionMaxLength)
            .IsRequired();
        
    }
}
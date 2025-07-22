using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class ContactEntityConfigurations : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Title)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();
    }
}
using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class ServiceConfigurations : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(LengthConstants.DescriptionMaxLength)
            .IsRequired();
    }
}
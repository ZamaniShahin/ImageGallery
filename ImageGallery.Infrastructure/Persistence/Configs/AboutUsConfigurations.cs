using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class AboutUsConfigurations : IEntityTypeConfiguration<AboutUsEntity>
{
    public void Configure(EntityTypeBuilder<AboutUsEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();

        builder.Property(x => x.H2Title)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(LengthConstants.DescriptionMaxLength)
            .IsRequired();

        
        builder
            .HasMany(x => x.Employees)
            .WithOne(x => x.AboutUs)
            .HasForeignKey(x => x.AboutUsId);
        
        builder
            .HasMany(x => x.ContactEntities)
            .WithOne(x => x.AboutUsEntity)
            .HasForeignKey(x => x.AboutUsId);

    }
}
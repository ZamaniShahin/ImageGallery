using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class CategoryConfigurations : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.Property(c => c.Title)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();
        
        builder.Property(c => c.Description)
            .HasMaxLength(LengthConstants.DescriptionMaxLength)
            .IsRequired();
        
        builder
            .HasMany(x => x.Images)
            .WithOne(x => x.CategoryEntity)
            .HasForeignKey(x => x.CategoryId);
    }
}

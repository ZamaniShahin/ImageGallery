using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
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
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
    }
}
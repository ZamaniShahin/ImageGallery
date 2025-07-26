using ImageGallery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Persistence.Configs;

public class EmployeeEntityConfigurations : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(LengthConstants.TitleMaxLength)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(LengthConstants.DescriptionMaxLength)
            .IsRequired();
        
        builder.Property(e => e.ProfilePhoto)
            .IsRequired();
        
        builder.HasOne(e => e.AboutUs)
            .WithMany(e => e.Employees)
            .HasForeignKey(e => e.AboutUsId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
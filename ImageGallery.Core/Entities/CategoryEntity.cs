using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class CategoryEntity : BaseEntity
{
    public CategoryEntity(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void AddImage(ImageEntity imageEntity)
    {
        Images.Add(imageEntity);
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public ICollection<ImageEntity> Images { get; private set; }
}
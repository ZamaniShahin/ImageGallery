using System.Collections.Generic;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class CategoryEntity : BaseEntity
{
    public CategoryEntity(string title, string description)
    {
        Title = title;
        Description = description;
    }

    private CategoryEntity()
    {
    }

    public void AddImage(ImageEntity imageEntity)
    {
        Images.Add(imageEntity);
    }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public ICollection<ImageEntity> Images { get; private set; } = new List<ImageEntity>();
}

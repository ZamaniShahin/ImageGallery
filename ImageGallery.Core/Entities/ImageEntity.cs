using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class ImageEntity : BaseEntity
{
    public ImageEntity(byte[] content, string description)
    {
        Content = content;
        Description = description;
    }


    public byte[] Content { get; private set; }
    public string Description { get; private set; }

    public Guid CategoryId { get; private set; }
    public CategoryEntity CategoryEntity { get; private set; }
}
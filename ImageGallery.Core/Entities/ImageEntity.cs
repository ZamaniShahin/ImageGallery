using System;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class ImageEntity : BaseEntity
{
    public ImageEntity(Guid categoryId, byte[] content, string description)
    {
        CategoryId = categoryId;
        Content = content;
        Description = description;
    }
    public void Update(byte[] content, string description)
    {
        Content = content;
        Description = description;
        this.Modified();
    }

    private ImageEntity()
    {
    }

    public byte[] Content { get; private set; } = Array.Empty<byte>();

    public string Description { get; private set; } = string.Empty;

    public Guid CategoryId { get; private set; }

    public CategoryEntity CategoryEntity { get; private set; } = null!;

    public ICollection<CommentEntity> Comments { get; private set; }
}

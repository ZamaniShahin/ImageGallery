namespace ImageGallery.Core.Entities;

public class Image : BaseEntity
{
    public Image(byte[] content, string description)
    {
        Content = content;
        Description = description;
    }


    public byte[] Content { get; private set; }
    public string Description { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
}
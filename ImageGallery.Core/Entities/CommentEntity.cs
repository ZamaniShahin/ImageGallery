using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class CommentEntity : BaseEntity
{
    public CommentEntity(string subject, string body, Guid imageId)
    {
        Subject = subject;
        Body = body;
        ImageId = imageId;
    }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public Guid ImageId { get; private set; }
    public ImageEntity Image { get; private set; }
}
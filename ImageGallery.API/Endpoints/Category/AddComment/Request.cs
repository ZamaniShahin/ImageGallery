namespace ImageGallery.API.Endpoints.Category.AddComment;

public class Request
{
    public const string Route = "categories/comments/{ImageId}";
    public Guid ImageId { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }
}
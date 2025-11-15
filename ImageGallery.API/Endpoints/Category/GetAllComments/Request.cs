namespace ImageGallery.API.Endpoints.Category.GetAllComments;

public class Request
{
    public const string Route = "categories/comments/{ImageId}";
    public Guid ImageId { get; set; }
}
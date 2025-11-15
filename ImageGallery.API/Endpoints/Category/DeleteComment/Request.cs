namespace ImageGallery.API.Endpoints.Category.DeleteComment;

public class Request
{
    public const string Route = "categories/comments/{CommentId}";
    public Guid CommentId { get; set; }
}
namespace ImageGallery.API.Endpoints.Category.DeleteImage;

public class Request
{
    public const string Route = "categories/images/{id}";

    public Guid Id { get; set; }
}

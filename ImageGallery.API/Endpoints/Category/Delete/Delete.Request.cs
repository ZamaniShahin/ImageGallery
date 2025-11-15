namespace ImageGallery.API.Endpoints.Category.Delete;

public class Request
{
    public const string Route = "categories/{id}";

    public Guid Id { get; set; }
}

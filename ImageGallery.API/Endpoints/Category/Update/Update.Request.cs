namespace ImageGallery.API.Endpoints.Category.Update;

public class Request
{
    public const string Route = "categories/{id}";

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

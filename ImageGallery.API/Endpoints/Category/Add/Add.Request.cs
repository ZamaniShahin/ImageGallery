namespace ImageGallery.API.Endpoints.Category.Add;

public class Request
{
    public const string Route = "/categories";

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

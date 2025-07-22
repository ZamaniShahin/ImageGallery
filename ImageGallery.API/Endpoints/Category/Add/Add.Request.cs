namespace ImageGallery.API.Endpoints.Category.Add;

public class Request
{
    public const string Route = "Category";

    public string Title { get; set; }
    public string Description { get; set; }
}
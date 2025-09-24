namespace ImageGallery.API.Endpoints.Category.AddImage;

public class Request
{
    public const string Route = "categories/{Id}";
    public byte[] Content { get; set; }
    public Guid Id { get; set; }
    public string Description { get; set; }
}

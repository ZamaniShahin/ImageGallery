namespace ImageGallery.API.Endpoints.Category.UpdateImage;

public class Request
{
    public const string Route = "categories/images/{id}";

    public Guid Id { get; set; }
    public byte[] Content { get; private set; } = Array.Empty<byte>();

    public string Description { get; private set; } = string.Empty;
}

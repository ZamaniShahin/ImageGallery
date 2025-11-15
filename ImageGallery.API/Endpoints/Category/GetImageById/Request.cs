namespace ImageGallery.API.Endpoints.Category.GetImageById;

public class Request
{
    public const string Route = "categories/{ImageId}/image";
    public Guid ImageId { get; set; }
}
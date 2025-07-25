namespace ImageGallery.API.Endpoints.Service.Add;

public class Request
{
    public const string Route = "Service";

    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public byte[] Logo { get; set; }
}

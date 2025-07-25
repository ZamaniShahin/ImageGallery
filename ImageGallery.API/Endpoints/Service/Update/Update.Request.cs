namespace ImageGallery.API.Endpoints.Service.Update;

public class Request
{
    public const string Route = "Service";
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Decription { get; set; }
    public decimal Price { get; set; }
    public byte[] Logo { get; set; }
}

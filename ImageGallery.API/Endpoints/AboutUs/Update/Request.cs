namespace ImageGallery.API.Endpoints.AboutUs.Update;

public class Request
{
    public const string Route = "AboutUs";
    public string Title { get; set; }
    public string H2Title { get; set; }
    public string Description { get; private set; }
    public byte[] Image { get; set; }
}
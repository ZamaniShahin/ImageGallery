using System;

namespace ImageGallery.API.Endpoints.AboutUs.Update;

public class Request
{
    public const string Route = "about-us";

    public string Title { get; set; } = string.Empty;

    public string H2Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public byte[] Image { get; set; } = Array.Empty<byte>();
}

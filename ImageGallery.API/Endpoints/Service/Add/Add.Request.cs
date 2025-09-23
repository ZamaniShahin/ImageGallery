using System;

namespace ImageGallery.API.Endpoints.Service.Add;

public class Request
{
    public const string Route = "services";

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public byte[] Logo { get; set; } = Array.Empty<byte>();
}


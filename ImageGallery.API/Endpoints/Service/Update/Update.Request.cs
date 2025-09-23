using System;

namespace ImageGallery.API.Endpoints.Service.Update;

public class Request
{
    public const string Route = "/services/{id:guid}";

    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public byte[] Logo { get; set; } = Array.Empty<byte>();
}


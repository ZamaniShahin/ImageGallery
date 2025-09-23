using System;

namespace ImageGallery.API.Endpoints.Category.GetAllImages;

public class Request
{
    public const string Route = "/categories/{id:guid}/images";

    public Guid Id { get; set; }
}

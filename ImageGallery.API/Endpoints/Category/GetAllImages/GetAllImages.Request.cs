using System;

namespace ImageGallery.API.Endpoints.Category.GetAllImages;

public class Request
{
    public const string Route = "categories/{Id:guid}/images";

    public Guid Id { get; set; }
}

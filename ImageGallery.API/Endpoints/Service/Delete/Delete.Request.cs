using System;

namespace ImageGallery.API.Endpoints.Service.Delete;

public class Request
{
    public const string Route = "services/{Id:guid}";

    public Guid Id { get; set; }
}


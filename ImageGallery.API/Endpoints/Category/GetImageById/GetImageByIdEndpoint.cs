using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.GetImageById;

public sealed class GetImageByIdEndpoint : BaseEndpoint<Request, Result<ImageRecord>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get image by id";
            s.Description = "Get image by id";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<GetImageByIdHandler>();
        var result = await handler.ExecuteAsync(new Core.Services.Category.GetImageById(request.ImageId), ct);
        await SendAsync(result);
    }
}

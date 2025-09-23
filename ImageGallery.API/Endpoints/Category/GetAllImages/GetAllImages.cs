using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.GetAllImages;

public sealed class GetCategoryImagesEndpoint : BaseEndpoint<Request, Result<List<ImageRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get All Images of a Category";
            s.Description = "Get All Images of a Category";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<GetAllImagesHandler>();
        var result = await handler.ExecuteAsync(new GetAllImages(request.Id), ct);
        await SendAsync(result);
    }
}

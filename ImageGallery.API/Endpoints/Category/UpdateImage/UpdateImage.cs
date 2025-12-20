using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.UpdateImage;

public sealed class UpdateCategoryEndpoint
    : BaseEndpoint<Request, Result<Guid>>
{
    public override void Configure()
    {
        Put(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Roles(Shared.Roles.Admin);
        Summary(s =>
        {
            s.Summary = "Update Image";
            s.Description = "Update Image";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<UpdateImageHandler>();
        var command = new Core.Services.Category.UpdateImage(request.Id, request.Content, request.Description);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

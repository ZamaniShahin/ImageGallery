using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.DeleteImage;

public sealed class DeleteImageEndpoint
    : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Delete(Request.Route);
        DontAutoTag();
        Roles(Shared.Roles.Admin);
        // AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Delete Image";
            s.Description = "Delete Image";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<DeleteImageHandler>();
        var command = new Core.Services.Category.DeleteImage(request.Id);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.Delete;

public sealed class DeleteCategoryEndpoint
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
            s.Summary = "Delete Category";
            s.Description = "Delete Category";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<DeleteHandler>();
        var command = new Core.Services.Category.Delete(request.Id);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

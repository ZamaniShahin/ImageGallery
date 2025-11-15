using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.AddImage;

public sealed class AddImageEndpoint
    : BaseEndpoint<Request, Result<Guid>>
{
    public override void Configure()
    {
        Post(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Roles(Shared.Roles.Admin);
        Summary(s =>
        {
            s.Summary = "Add Image into a category";
            s.Description = "Add Image into a category";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<AddImageHandler>();
        var command = new Core.Services.Category.AddImage(request.Id, request.Content, request.Description);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

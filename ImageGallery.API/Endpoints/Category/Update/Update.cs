using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.Update;

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
            s.Summary = "Update Category";
            s.Description = "Update Category";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<UpdateHandler>();
        var command = new Core.Services.Category.Update(request.Id, request.Title, request.Description);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

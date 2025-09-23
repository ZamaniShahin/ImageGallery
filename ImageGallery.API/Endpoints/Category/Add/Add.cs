using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.Add;

public sealed class AddCategoryEndpoint
    : BaseEndpoint<Request, Result<Guid>>
{
    public override void Configure()
    {
        Post(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Add Category";
            s.Description = "Add Category";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<AddHandler>();
        var command = new Add(request.Title, request.Description);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

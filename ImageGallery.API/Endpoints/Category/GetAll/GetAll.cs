using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.GetAll;

public sealed class GetAllCategoriesEndpoint : BaseEndpoint<Request, Result<List<CategoryRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get All Categories";
            s.Description = "Get All Categories";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<GetAllHandler>();
        var result = await handler.ExecuteAsync(new GetAll(), ct);

        await SendAsync(result);
    }
}

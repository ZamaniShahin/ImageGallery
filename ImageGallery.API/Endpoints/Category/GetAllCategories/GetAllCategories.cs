using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.GetAllCategories;

public sealed class GetAllCategoriesEndpoint : BaseEndpoint<EmptyRequest, Result<List<CategoryRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        //todo: remove allow anonymous from needed endpoints
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get All Categories";
            s.Description = "Get All Categories";
        });
    }

    protected override async Task ExecuteAsync(EmptyRequest request, CancellationToken ct)
    {
        var handler = Resolve<GetAllHandler>();
        var result = await handler.ExecuteAsync(new GetAll(), ct);

        await SendAsync(result);
    }
}
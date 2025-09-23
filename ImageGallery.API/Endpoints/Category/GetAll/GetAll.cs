namespace ImageGallery.API.Endpoints.Category.GetAll;

public class GetAll : BaseEndpoint<EmptyRequest, Result<List<CategoryRecord>>>
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

    protected override async Task ExecuteAsync(EmptyRequest request, CancellationToken ct)
    {
        var result = await new Core.Services.Category.GetAll()
            .ExecuteAsync(ct);

        await SendAsync(result);
    }
}

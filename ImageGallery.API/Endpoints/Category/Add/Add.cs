namespace ImageGallery.API.Endpoints.Category.Add;

public class Add
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
        var command = new Core.Services.Category.Add(request.Title, request.Description);
        var result = await command.ExecuteAsync(ct);
        await SendAsync(result);
    }
}
namespace ImageGallery.API.Endpoints.Service.Add;

public class Add : BaseEndpoint<Request, Result<Guid>>
{
    public override void Configure()
    {
        Post(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Add Service";
            s.Description = "Add Service";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var command =
            new Core.Services.Service.Add(request.Title, request.Description, request.Price, request.Logo);
        var result = await command.ExecuteAsync(ct);
        await SendAsync(result);
    }
}
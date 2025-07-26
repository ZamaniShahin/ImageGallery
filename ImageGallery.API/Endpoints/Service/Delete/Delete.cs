namespace ImageGallery.API.Endpoints.Service.Delete;

public class Delete : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Delete(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Delete Service By Id";
            s.Description = "Delete Service By Id";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var command = new Core.Services.Service.Delete(request.Id);
        var result = await command.ExecuteAsync(ct);
        await SendAsync(result);
    }
}
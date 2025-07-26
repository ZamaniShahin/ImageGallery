namespace ImageGallery.API.Endpoints.Service.Update;

public class Update : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Post(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Update Service";
            s.Description = "Update Service";
        });
    }
    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var command = new Core.Services.Service.Update(request.Id, request.Title, request.Description, request.Price, request.Logo);
        var result = await command.ExecuteAsync(ct);
        await SendAsync(result);
    }
}
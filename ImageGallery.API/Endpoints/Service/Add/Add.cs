using ImageGallery.Core.Services.Service;

namespace ImageGallery.API.Endpoints.Service.Add;

public sealed class AddServiceEndpoint : BaseEndpoint<Request, Result<Guid>>
{
    public override void Configure()
    {
        Post(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Add Service";
            s.Description = "Add Service";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<AddHandler>();
        var command = new Core.Services.Service.Add(request.Title, request.Description, request.Price, request.Logo);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

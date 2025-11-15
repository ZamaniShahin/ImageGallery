using ImageGallery.Core.Services.Service;

namespace ImageGallery.API.Endpoints.Service.Update;

public sealed class UpdateServiceEndpoint : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Put(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Roles(Shared.Roles.Admin);
        Summary(s =>
        {
            s.Summary = "Update Service";
            s.Description = "Update Service";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<UpdateHandler>();
        var command = new Core.Services.Service.Update(request.Id, request.Title, request.Description, request.Price, request.Logo);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

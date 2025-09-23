using ImageGallery.Core.Services.Service;

namespace ImageGallery.API.Endpoints.Service.Delete;

public sealed class DeleteServiceEndpoint : BaseEndpoint<Request, Result<bool>>
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
        var handler = Resolve<DeleteHandler>();
        var command = new Delete(request.Id);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

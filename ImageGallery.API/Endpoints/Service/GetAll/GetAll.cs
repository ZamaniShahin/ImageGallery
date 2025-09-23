using ImageGallery.Core.Services.Service;

namespace ImageGallery.API.Endpoints.Service.GetAll;

public sealed class GetAllServicesEndpoint : BaseEndpoint<Request, Result<List<ServiceRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get Services";
            s.Description = "Get Services";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<GetAllHandler>();
        var result = await handler.ExecuteAsync(new GetAll(), ct);
        await SendAsync(result);
    }
}

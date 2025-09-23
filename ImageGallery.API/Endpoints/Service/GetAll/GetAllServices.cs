using ImageGallery.Core.Services.Service;

namespace ImageGallery.API.Endpoints.Service.GetAll;

public sealed class GetAllServicesEndpoint : BaseEndpoint<EmptyRequest, Result<List<ServiceRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        AllowAnonymous();
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get Services";
            s.Description = "Get Services";
        });
    }

    protected override async Task ExecuteAsync(EmptyRequest request, CancellationToken ct)
    {
        var handler = Resolve<GetAllHandler>();
        var result = await handler.ExecuteAsync(new Core.Services.Service.GetAll(), ct);
        await SendAsync(result);
    }
}
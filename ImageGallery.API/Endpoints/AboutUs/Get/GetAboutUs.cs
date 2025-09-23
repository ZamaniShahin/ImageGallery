using ImageGallery.Core.Services.GetAboutUs;

namespace ImageGallery.API.Endpoints.AboutUs.Get;

public sealed class GetAboutUsEndpoint : BaseEndpoint<Request, Result<AboutUsRecord>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get About Us";
            s.Description = "Get About Us Page Info";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<GetHandler>();
        var result = await handler.ExecuteAsync(new Get(), ct);
        await SendAsync(result);
    }
}

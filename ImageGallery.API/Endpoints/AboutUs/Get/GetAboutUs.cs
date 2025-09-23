using ImageGallery.Core.Services.GetAboutUs;

namespace ImageGallery.API.Endpoints.AboutUs.Get;

public sealed class GetAboutUsEndpoint : BaseEndpoint<EmptyRequest, Result<AboutUsRecord>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get About Us";
            s.Description = "Get About Us Page Info";
        });
    }

    protected override async Task ExecuteAsync(EmptyRequest request, CancellationToken ct)
    {
        var handler = Resolve<GetHandler>();
        var result = await handler.ExecuteAsync(new Core.Services.GetAboutUs.Get(), ct);
        await SendAsync(result);
    }
}

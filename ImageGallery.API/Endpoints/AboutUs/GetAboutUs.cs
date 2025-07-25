using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.API.Endpoints.AboutUs;

public class GetAboutUs : BaseEndpoint<Request, Result<AboutUsRecord>>
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
        var result = await new Core.Services.GetAboutUs.Get().ExecuteAsync(ct);
        await SendAsync(result);
    }
}
using ImageGallery.Core.Services.GetAboutUs;

namespace ImageGallery.API.Endpoints.AboutUs.Update;

public sealed class UpdateAboutUsEndpoint : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Put(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        AllowAnonymous();
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Update About Us";
            s.Description = "Update About Us Page Info";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<UpdateHandler>();
        var command = new Core.Services.GetAboutUs.Update(request.Title, request.H2Title, request.Description, request.Image);
        var result = await handler.ExecuteAsync(command, ct);

        await SendAsync(result);
    }
}

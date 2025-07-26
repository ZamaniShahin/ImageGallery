namespace ImageGallery.API.Endpoints.AboutUs.Update;

public class Update : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Put(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Update About Us";
            s.Description = "Update About Us Page Info";
        });
    }
    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await
            new Core.Services.GetAboutUs.Update(request.Title, request.H2Title, request.Description, request.Image)
                .ExecuteAsync(ct);

        await SendAsync(result);
    }
}
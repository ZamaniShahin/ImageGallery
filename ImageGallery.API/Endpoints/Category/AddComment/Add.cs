using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.AddComment;

public sealed class AddCommentEndpoint
    : BaseEndpoint<Request, Result<Guid>>
{
    public override void Configure()
    {
        Post(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Add Comment for an image";
            s.Description = "Add Comment for an image";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<AddCommentHandler>();
        var command = new Core.Services.Category.AddComment(request.ImageId, request.Subject, request.Body);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

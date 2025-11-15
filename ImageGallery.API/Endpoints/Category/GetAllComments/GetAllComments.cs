using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.GetAllComments;

public sealed class GetAllCommentsEndpoint
    : BaseEndpoint<Request, Result<List<CommentRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "get all comments";
            s.Description = "get all comments";
        });
    }

    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<GetAllCommentsHandler>();
        var command = new Core.Services.Category.GetAllComments(request.ImageId);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}

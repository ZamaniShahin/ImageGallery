using ImageGallery.Core.Services.Category;

namespace ImageGallery.API.Endpoints.Category.DeleteComment;

public class DeleteCommentEndpoint
    : BaseEndpoint<Request, Result<bool>>
{
    public override void Configure()
    {
        Delete(Request.Route);
        PreProcessor<ValidationPreprocessor<Request>>();
        DontAutoTag();
        Roles(Shared.Roles.Admin);
        Summary(s =>
        {
            s.Summary = "delete a comment by admin";
            s.Description = "delete a comment by admin";
        });
    }
    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var handler = Resolve<DeleteCommentHandler>();
        var command = new Core.Services.Category.DeleteComment(request.CommentId);
        var result = await handler.ExecuteAsync(command, ct);
        await SendAsync(result);
    }
}
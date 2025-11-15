using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record DeleteComment(Guid Id) : ICommand<Result<bool>>;

public sealed class DeleteCommentHandler(IAppRepository<CommentEntity> repository) : ICommandHandler<DeleteComment, Result<bool>>
{
    private readonly IAppRepository<CommentEntity> _repository = repository;

    public async Task<Result<bool>> ExecuteAsync(DeleteComment command, CancellationToken ct)
    {
        var comment = await _repository.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: ct);

        if (comment is null)
        {
            return Result.Fail<bool>("comment not found");
        }
        await _repository.RemoveAsync(comment);
        await _repository.SaveChangesAsync();
        
        return Result.Ok();
    }
}

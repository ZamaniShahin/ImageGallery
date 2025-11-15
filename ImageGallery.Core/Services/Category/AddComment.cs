using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record AddComment(Guid Id, string Subject, string Body) : ICommand<Result<Guid>>;

public sealed class AddCommentHandler(IAppRepository<ImageEntity> repository, IAppRepository<CommentEntity> commentRepository) : ICommandHandler<AddComment, Result<Guid>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;
    private readonly IAppRepository<CommentEntity> _commentRepository = commentRepository;

    public async Task<Result<Guid>> ExecuteAsync(AddComment command, CancellationToken ct)
    {
        var image = await _repository.FirstOrDefaultAsync(x => x.Id == command.Id,
            true, 
            ct, 
            x => x.Comments);

        if (image is null)
        {
            return Result.Fail<Guid>("image not found");
        }
        
        
        var comment = new CommentEntity(command.Body, command.Subject, image.Id);
        await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveChangesAsync();

        return Result.Ok(comment.Id);
    }
}

using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record GetAllComments(Guid Id) : ICommand<Result<List<CommentRecord>>>;

public sealed class GetAllCommentsHandler(IAppRepository<ImageEntity> repository) : ICommandHandler<GetAllComments, Result<List<CommentRecord>>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;

    public async Task<Result<List<CommentRecord>>> ExecuteAsync(GetAllComments command, CancellationToken ct)
    {
        var image = await _repository.FirstOrDefaultAsync(x => x.Id == command.Id,
            true, 
            ct, 
            x => x.Comments);

        if (image is null)
        {
            return Result.Fail<List<CommentRecord>>("image not found");
        }
        var result = image.Comments
            .Select(x => new CommentRecord(x.Id, x.Subject, x.Body))
            .ToList();
        
        return Result.Ok(result);
    }
}

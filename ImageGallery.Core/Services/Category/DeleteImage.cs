using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record DeleteImage(Guid Id) : ICommand<Result<bool>>;
public sealed class DeleteImageHandler(IAppRepository<ImageEntity> repository) : ICommandHandler<DeleteImage, Result<bool>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;

    public async Task<Result<bool>> ExecuteAsync(DeleteImage command, CancellationToken ct)
    {
        var image = await _repository.GetByIdAsync(command.Id);
        if (image is null)
        {
            return Result.Fail<bool>("image not found");
        }
        await _repository.RemoveAsync(image);
        await _repository.SaveChangesAsync();

        return Result.Ok(true);
    }
}

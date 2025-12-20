using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record UpdateImage(Guid Id, byte[] Content, string Description) : ICommand<Result<Guid>>;
public sealed class UpdateImageHandler(IAppRepository<ImageEntity> repository) : ICommandHandler<UpdateImage, Result<Guid>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;

    public async Task<Result<Guid>> ExecuteAsync(UpdateImage command, CancellationToken ct)
    {
        var image = await _repository.GetByIdAsync(command.Id);
        if (image is null)
        {
            return Result.Fail<Guid>("Image not found");
        }
        image.Update(command.Content, command.Description);
        await _repository.UpdateAsync(image);
        await _repository.SaveChangesAsync();

        return Result.Ok(image.Id);
    }
}

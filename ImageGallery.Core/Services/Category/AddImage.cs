using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record AddImage(Guid Id, byte[] Content, string Description) : ICommand<Result<Guid>>;

public sealed class AddImageHandler(IAppRepository<ImageEntity> repository) : ICommandHandler<AddImage, Result<Guid>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;

    public async Task<Result<Guid>> ExecuteAsync(AddImage command, CancellationToken ct)
    {
        var category = new ImageEntity(command.Id, command.Content, command.Description);
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return Result.Ok(category.Id);
    }
}

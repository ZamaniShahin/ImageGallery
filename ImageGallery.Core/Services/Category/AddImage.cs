using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record AddImage(Guid Id, byte[] Content, string Description) : ICommand<Result<Guid>>;

public sealed class AddImageHandler(IAppRepository<ImageEntity> repository,
    IAppRepository<CategoryEntity> categoryRepository) : ICommandHandler<AddImage, Result<Guid>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;
    private readonly IAppRepository<CategoryEntity> _categoryRepository = categoryRepository;

    public async Task<Result<Guid>> ExecuteAsync(AddImage command, CancellationToken ct)
    {
        var category = await _categoryRepository.FirstOrDefaultAsync(x => x.Id == command.Id, true, ct);
        if (category is null)
        {
            return Result.Fail<Guid>("category not found");
        }
        var image = new ImageEntity(category.Id, command.Content, command.Description);
        await _repository.AddAsync(image);
        await _repository.SaveChangesAsync();

        return Result.Ok(image.Id);
    }
}

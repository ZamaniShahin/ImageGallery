using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record GetAllImages(Guid Id) : ICommand<Result<List<ImageRecord>>>;

public sealed class GetAllImagesHandler(IAppRepository<CategoryEntity> repository)
    : ICommandHandler<GetAllImages, Result<List<ImageRecord>>>
{
    private readonly IAppRepository<CategoryEntity> _repository = repository;

    public async Task<Result<List<ImageRecord>>> ExecuteAsync(GetAllImages command, CancellationToken ct)
    {
        var category = await _repository.FirstOrDefaultAsync(
            x => x.Id == command.Id,
            true,
            ct,
            x => x.Images);

        if (category is null)
        {
            return Result.Fail<List<ImageRecord>>("Category not found");
        }

        var images = category.Images
            .Select(i => new ImageRecord(i.Id, i.Description, i.Content))
            .ToList();

        return Result.Ok(images);
    }
}

using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record GetAllImages(Guid Id) : ICommand<Result<List<ImageRecord>>>;

public sealed class GetAllImagesHandler(IAppRepository<CategoryEntity> repository) : ICommandHandler<GetAllImages, Result<List<ImageRecord>>>
{
    private readonly IAppRepository<CategoryEntity> _repository = repository;

    public async Task<Result<List<ImageRecord>>> ExecuteAsync(GetAllImages command, CancellationToken ct)
    {
        var query = await _repository
                .GetWithIncludesAsync(true, x => x.Images);
        var images = query
            .Where(x => x.Id == command.Id)
            .SelectMany(x => x.Images.Select(i => new ImageRecord(i.Id, i.Description, i.Content)))
            .ToList();
        
            return images.Count == 0 ? Result.Fail("User Not Found") : Result.Ok(images);
    }
}
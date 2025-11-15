using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record GetImageById(Guid Id) : ICommand<Result<ImageRecord>>;

public sealed class GetImageByIdHandler(IAppRepository<ImageEntity> repository)
    : ICommandHandler<GetImageById, Result<ImageRecord>>
{
    private readonly IAppRepository<ImageEntity> _repository = repository;

    public async Task<Result<ImageRecord>> ExecuteAsync(GetImageById command, CancellationToken ct)
    {
        var image = await _repository.FirstOrDefaultAsync(
            x => x.Id == command.Id,
            true,
            ct);

        if (image is null)
        {
            return Result.Fail<ImageRecord>("Image not found");
        }

        return Result.Ok(new ImageRecord(image.Id, image.Description, image.Content));
    }
}
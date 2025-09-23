using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.GetAboutUs;

public record Update(string Title, string H2Title, string Description, byte[] Image) : ICommand<Result<bool>>;

public sealed class UpdateHandler(IAppRepository<AboutUsEntity> repository) : ICommandHandler<Update, Result<bool>>
{
    private readonly IAppRepository<AboutUsEntity> _repository = repository;

    public async Task<Result<bool>> ExecuteAsync(Update command, CancellationToken ct)
    {
        var about = await _repository.SingleOrDefaultAsync(
            query => query,
            cancellationToken: ct);

        if (about is null)
        {
            return Result.Fail<bool>("About us information not found");
        }

        about.Update(command.Title, command.H2Title, command.Description, command.Image);

        await _repository.UpdateAsync(about);
        await _repository.SaveChangesAsync();

        return Result.Ok(true);
    }
}

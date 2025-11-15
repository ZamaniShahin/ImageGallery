using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record Update(Guid Id, string Title, string Description) : ICommand<Result<Guid>>;
public sealed class UpdateHandler(IAppRepository<CategoryEntity> repository) : ICommandHandler<Update, Result<Guid>>
{
    private readonly IAppRepository<CategoryEntity> _repository = repository;

    public async Task<Result<Guid>> ExecuteAsync(Update command, CancellationToken ct)
    {
        var category = await _repository.GetByIdAsync(command.Id);
        if (category is null)
        {
            return Result.Fail<Guid>("Category not found");
        }
        category.Update(command.Title, command.Description);
        await _repository.UpdateAsync(category);
        await _repository.SaveChangesAsync();

        return Result.Ok(category.Id);
    }
}

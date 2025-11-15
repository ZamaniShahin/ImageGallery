using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record Delete(Guid Id) : ICommand<Result<bool>>;
public sealed class DeleteHandler(IAppRepository<CategoryEntity> repository) : ICommandHandler<Delete, Result<bool>>
{
    private readonly IAppRepository<CategoryEntity> _repository = repository;

    public async Task<Result<bool>> ExecuteAsync(Delete command, CancellationToken ct)
    {
        var category = await _repository.GetByIdAsync(command.Id);
        if (category is null)
        {
            return Result.Fail<bool>("Category not found");
        }
        await _repository.RemoveAsync(category);
        await _repository.SaveChangesAsync();

        return Result.Ok(true);
    }
}

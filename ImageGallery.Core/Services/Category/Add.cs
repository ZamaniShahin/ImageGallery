using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Category;

public record Add(string Title, string Description) : ICommand<Result<Guid>>;
public sealed class AddHandler(IAppRepository<CategoryEntity> repository) : ICommandHandler<Add, Result<Guid>>
{
    private readonly IAppRepository<CategoryEntity> _repository = repository;
    public async Task<Result<Guid>> ExecuteAsync(Add command, CancellationToken ct)
    {
        var category = new CategoryEntity(command.Title, command.Description);
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        return Result.Ok(category.Id);
    }
}
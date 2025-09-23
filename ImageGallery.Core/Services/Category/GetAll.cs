using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Core.Services.Category;

public record GetAll() : ICommand<Result<List<CategoryRecord>>>;

public sealed class GetAllHandler(IAppRepository<CategoryEntity> repository) : ICommandHandler<GetAll, Result<List<CategoryRecord>>>
{    
    private readonly IAppRepository<CategoryEntity> _repository = repository;

    public async Task<Result<List<CategoryRecord>>> ExecuteAsync(GetAll command, CancellationToken ct)
    {
        var categories = await _repository
            .GetAsQuery(true)
            .Select(x => new CategoryRecord(x.Id, x.Title, x.Description))
            .ToListAsync(ct);

        return Result.Ok(categories);
    }
}

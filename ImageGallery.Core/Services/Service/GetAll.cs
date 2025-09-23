using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Core.Services.Service;

public record GetAll() : ICommand<Result<List<ServiceRecord>>>;

public sealed class GetAllHandler(IAppRepository<ServiceEntity> repository)
    : ICommandHandler<GetAll, Result<List<ServiceRecord>>>
{
    private readonly IAppRepository<ServiceEntity> _repository = repository;

    public async Task<Result<List<ServiceRecord>>> ExecuteAsync(GetAll command, CancellationToken ct)
    {
        var services = await _repository.ListAsync(
            query => query.Select(x => new ServiceRecord(x.Id, x.Title, x.Description, x.Price, x.Logo)),
            true,
            ct);

        return Result.Ok(services);
    }
}

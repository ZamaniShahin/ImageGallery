using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Service;

public record GetAll() : ICommand<Result<List<ServiceRecord>>>;
public sealed class GetAllHandler(IAppRepository<ServiceEntity> repository) : ICommandHandler<GetAll, Result<List<ServiceRecord>>>
{
    private readonly IAppRepository<ServiceEntity> _repository = repository;
    public async Task<Result<List<ServiceRecord>>> ExecuteAsync(GetAll command, CancellationToken ct)
    {
        var services = _repository.GetAsQuery(true)
            .Select(x => new ServiceRecord(x.Id, x.Title, x.Description, x.Price, x.Logo))
            .ToList();
        return Result.Ok(services);
    }
}
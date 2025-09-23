using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Service;

public record Delete(Guid Id) : ICommand<Result<bool>>;

public sealed class DeleteHandler(IAppRepository<ServiceEntity> repository) : ICommandHandler<Delete, Result<bool>>
{
    private readonly IAppRepository<ServiceEntity> _repository = repository;

    public async Task<Result<bool>> ExecuteAsync(Delete command, CancellationToken ct)
    {
        var service = await _repository.GetByIdAsync(command.Id);
        if (service is null)
        {
            return Result.Fail<bool>("Service not found");
        }

        await _repository.RemoveAsync(service);
        await _repository.SaveChangesAsync();

        return Result.Ok(true);
    }
}

using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Service;

public record Add(string Title, string Description, decimal Price, byte[] Logo) : ICommand<Result<Guid>>;

public sealed class AddHandler(IAppRepository<ServiceEntity> repository) : ICommandHandler<Add, Result<Guid>>
{
    private readonly IAppRepository<ServiceEntity> _repository = repository;

    public async Task<Result<Guid>> ExecuteAsync(Add command, CancellationToken ct)
    {
        var service = new ServiceEntity(command.Title, command.Description, command.Price, command.Logo);
        await _repository.AddAsync(service);
        await _repository.SaveChangesAsync();

        return Result.Ok(service.Id);
    }
}

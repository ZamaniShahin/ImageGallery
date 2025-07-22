using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.Service;

public record Update(Guid Id, string Title, string Description, decimal Price, byte[] Logo) : ICommand<Result<bool>>;

public sealed class UpdateHandler(IAppRepository<ServiceEntity> repository) : ICommandHandler<Update, Result<bool>>
{
    private readonly IAppRepository<ServiceEntity> _repository = repository;
    public async Task<Result<bool>> ExecuteAsync(Update command, CancellationToken ct)
    {
        var service = await _repository.GetByIdAsync(command.Id);
        if (service is null)
            return Result.Fail<bool>("Service not found");
        service.Update(command.Title, command.Description, command.Price, command.Logo);
        await _repository.UpdateAsync(service);
        await _repository.SaveChangesAsync();
        return Result.Ok(true);
    }
}
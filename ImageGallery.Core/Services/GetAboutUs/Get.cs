using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.GetAboutUs;

public record Get() : ICommand<Result<AboutUsRecord>>;

public sealed class GetHandler(IAppRepository<AboutUsEntity> repository) : ICommandHandler<Get, Result<AboutUsRecord>>
{
    private readonly IAppRepository<AboutUsEntity> _repository = repository;

    public async Task<Result<AboutUsRecord>> ExecuteAsync(Get command, CancellationToken ct)
    {
        var entity = await _repository.FirstOrDefaultAsync(x => true,
            true,
            ct,
            x => x.Employees);

        var about = new AboutUsRecord(
            entity.Title,
            entity.H2Title,
            entity.Description,
            entity.Image,
            entity.Employees
                .Select(e => new EmployeeRecord(
                    e.Id,
                    e.Title,
                    e.Description,
                    e.ProfilePhoto)).ToList());

        if (about is null)
        {
            return Result.Fail<AboutUsRecord>("About us information not found");
        }

        return Result.Ok(about);
    }
}

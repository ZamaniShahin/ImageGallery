using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Entities;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Services.GetAboutUs;

public record Get() : ICommand<Result<AboutUsRecord>>;

public sealed class GetUsHandler(IAppRepository<AboutUsEntity> repository) : ICommandHandler<Get, Result<AboutUsRecord>>
{
    private readonly IAppRepository<AboutUsEntity> _repository = repository;

    public async Task<Result<AboutUsRecord>> ExecuteAsync(Get command, CancellationToken ct)
    {
        var query = await _repository.GetWithIncludesAsync(true, x => x.Employees);

        var about = query
            .Select(x => new AboutUsRecord(x.Title,
                x.H2Title,
                x.Description,
                x.Image,
                x.Employees.Select(e => new EmployeeRecord(e.Id, e.Title, e.Description, e.ProfilePhoto)).ToList()))
            .Single();
        return Result.Ok(about);
    }
}
using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.API.Endpoints.Service.GetAll;

public class GetAll : BaseEndpoint<EmptyRequest, Result<List<ServiceRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get Services";
            s.Description = "Get Services";
        });
    }
    protected override async Task ExecuteAsync(EmptyRequest request, CancellationToken ct)
    {
        var result = await new Core.Services.Service.GetAll().ExecuteAsync(ct);
        await SendAsync(result);
    }
}
using FastEndpoints;
using FluentResults;
using ImageGallery.Core.Records;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.API.Endpoints.Category.GetAllImages;

public class GetAllImages : BaseEndpoint<Request, Result<List<ImageRecord>>>
{
    public override void Configure()
    {
        Get(Request.Route);
        DontAutoTag();
        Summary(s =>
        {
            s.Summary = "Get All Images of a Category";
            s.Description = "Get All Images of a Category";
        });
    }
    protected override async Task ExecuteAsync(Request request, CancellationToken ct)
    {
        var result = await new Core.Services.Category.GetAllImages(request.Id)
            .ExecuteAsync(ct);
        await SendAsync(result);
    }
}
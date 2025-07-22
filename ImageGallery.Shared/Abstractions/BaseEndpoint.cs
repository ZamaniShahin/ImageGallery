using FastEndpoints;
using System.Security.Claims;

namespace ImageGallery.Shared.Abstractions;

public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    protected ClaimsPrincipal UserPrincipal => HttpContext.User;

    protected Guid? UserId =>
        Guid.TryParse(UserPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

    public override async Task HandleAsync(TRequest req, CancellationToken ct)
    {
        await ExecuteAsync(req, ct);
    }

    protected abstract Task ExecuteAsync(TRequest request, CancellationToken ct);
}
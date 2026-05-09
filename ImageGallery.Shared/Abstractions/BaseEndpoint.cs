using FastEndpoints;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

    protected TService Resolve<TService>() where TService : notnull =>
        HttpContext.RequestServices.GetRequiredService<TService>();

    protected abstract Task ExecuteAsync(TRequest request, CancellationToken ct);

    protected async Task SendResultAsync<T>(Result<T> result, CancellationToken ct = default)
    {
        if (result.IsFailed)
        {
            HttpContext.Response.StatusCode = 400;
            await HttpContext.Response.WriteAsJsonAsync(
                new { errors = result.Errors.Select(e => e.Message) }, ct);
            return;
        }
        HttpContext.Response.StatusCode = 200;
        await HttpContext.Response.WriteAsJsonAsync(result.Value, ct);
    }
}

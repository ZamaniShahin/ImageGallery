using System.Text.Json;
using FluentValidation;

namespace ImageGallery.API.Preprocessors;

public interface IValidationPreProcessor{}
public class ValidationPreprocessor<TRequest> : IPreProcessor<TRequest> where TRequest : notnull
{
    public async Task PreProcessAsync(IPreProcessorContext<TRequest> ctx, CancellationToken ct)
    {
        var validator = ctx.HttpContext.RequestServices.GetService<IValidator<TRequest>>();

        if (validator is not null && ctx.Request is not null)
        {
            var validationResult = await validator.ValidateAsync(ctx.Request, ct);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var jsonResponse = JsonSerializer.Serialize(errors);
                var response = ctx.HttpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status400BadRequest;

                await response.WriteAsync(jsonResponse, ct);
                return;
            }
        }
    }
}
using ImageGallery.Infrastructure.Persistence.Configs;

namespace ImageGallery.API.Endpoints.Service.Add;

public class Validations : AbstractValidator<Request>
{
    public Validations()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(LengthConstants.TitleMaxLength)
            .WithMessage("Title must be between 0 and 50");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(LengthConstants.DescriptionMaxLength)
            .WithMessage("Description must be between 0 and 500");
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .LessThan(100_000_000)
            .WithMessage("Price must be between 0 and 100_000_000");
    }
}

using ImageGallery.Infrastructure.Persistence.Configs;

namespace ImageGallery.API.Endpoints.Category.Add;

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
        
    }
}
using ImageGallery.Infrastructure.Persistence.Configs;

namespace ImageGallery.API.Endpoints.Category.UpdateImage;

public class Validations : AbstractValidator<Request>
{
    public Validations()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(LengthConstants.DescriptionMaxLength)
            .WithMessage("Description must be between 0 and 500");
        
    }
}

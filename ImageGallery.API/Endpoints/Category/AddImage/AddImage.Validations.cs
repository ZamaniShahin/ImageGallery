using ImageGallery.Infrastructure.Persistence.Configs;

namespace ImageGallery.API.Endpoints.Category.AddImage;

public class Validations : AbstractValidator<Request>
{
    public Validations()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Please specify a image file");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(LengthConstants.DescriptionMaxLength)
            .WithMessage("Description must be between 0 and 500");
        
    }
}

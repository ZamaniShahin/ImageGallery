using ImageGallery.Infrastructure.Persistence.Configs;

namespace ImageGallery.API.Endpoints.AboutUs.Update;

public class Validations : AbstractValidator<Request>
{
    public Validations()
    {
        RuleFor(x => x.Title)
            .MaximumLength(LengthConstants.TitleMaxLength)
            .NotEmpty()
            .WithMessage("Title must be between 0 and 50");
        RuleFor(x => x.H2Title)
            .MaximumLength(LengthConstants.TitleMaxLength)
            .NotEmpty()
            .WithMessage("H2Title must be between 0 and 50");
        RuleFor(x => x.Description)
            .MaximumLength(LengthConstants.DescriptionMaxLength)
            .NotEmpty()
            .WithMessage("Description must be between 0 and 500");
    }
}
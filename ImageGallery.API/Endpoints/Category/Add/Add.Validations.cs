using FluentValidation;
using ImageGallery.Infrastructure.Persistence.Configs;

namespace ImageGallery.API.Endpoints.Category.Add;

public class Validations : AbstractValidator<Request>
{
    public Validations()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(LengthConstants.TitleMaxLength);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(LengthConstants.DescriptionMaxLength);
        
    }
}
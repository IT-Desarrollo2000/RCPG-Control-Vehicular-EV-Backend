using Domain.DTOs.Requests;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class ProfileImageValidator : AbstractValidator<ProfileImageRequest>
    {
        public ProfileImageValidator()
        {
            RuleFor(r => r.ImageFile)
                .SetValidator(new ImageFileValidator());
        }
    }
}

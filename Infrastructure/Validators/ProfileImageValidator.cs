using Domain.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

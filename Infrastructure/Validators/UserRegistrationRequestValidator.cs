﻿using Domain.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class UserRegistrationRequestValidator : AbstractValidator<AppUserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("La contraseña no puede ser menor a 8 caracteres");

            RuleFor(request => request.FirstName)
                .NotEmpty();

            RuleFor(Request => Request.LastNameP)
                .NotEmpty();

            RuleFor(Request => Request.LastNameM)
                .NotEmpty();

            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(request => request.RegistrationType)
                .NotNull()
                .WithMessage("Es obligatorio establecer el tipo de registro del usuario");

        }
    }
}

using Domain.DTOs.Requests;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(t => t.Token)
                .NotEmpty();

            RuleFor(t => t.RefreshToken)
                .NotEmpty();
        }
    }
}

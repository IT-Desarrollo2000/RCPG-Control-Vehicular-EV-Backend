using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Validators
{
    public class ImageFileValidator : AbstractValidator<IFormFile>
    {
        private int MaxFileSyzeMb = 5;

        public ImageFileValidator()
        {
            RuleFor(x => x.Length)
                .Must(IsValidFileSize)
                .WithMessage($"El archivo supera el limite de {MaxFileSyzeMb}Mb");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("El archivo no corresponde a un tipo de imagen valido");

        }

        private bool IsValidFileSize(long fileSize)
        {
            if ((fileSize / 1048576.0) > MaxFileSyzeMb)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

using FluentValidation;
using MotoRental.Application.Commands;

namespace MotoRental.Application.Validators
{
    public class UploadCnhCommandValidator : AbstractValidator<UploadCnhCommand>
    {
        public UploadCnhCommandValidator()
        {
            RuleFor(x => x.DeliveryPersonId)
                .NotEmpty().WithMessage("The DeliveryPersonId is required.");

            RuleFor(x => x.Image)
                .NotNull().WithMessage("The CNH image is required.")
                .Must(file => file.ContentType == "image/png" || file.ContentType == "image/bmp")
                .WithMessage("Only PNG or BMP images are allowed.");
        }
    }
}

using FluentValidation;
using MotoRental.Application.Commands;

namespace MotoRental.Application.Validators
{
    public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
    {
        public CreateRentalCommandValidator()
        {
            RuleFor(x => x.DeliveryPersonId)
                .NotEmpty().WithMessage("The DeliveryPersonId is required.");

            RuleFor(x => x.MotoId)
                .NotEmpty().WithMessage("The MotoId is required.");

            RuleFor(x => x.Plan)
                .IsInEnum().WithMessage("The rental plan is invalid.");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("The rental start date must be in the future.");
        }
    }
}

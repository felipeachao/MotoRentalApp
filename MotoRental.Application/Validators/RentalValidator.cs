using FluentValidation;
using MotoRental.Domain.Entities;

namespace MotoRental.Application.Validators
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.DeliveryPersonId).NotEmpty().WithMessage("Delivery person is required.");
            RuleFor(r => r.MotoId).NotEmpty().WithMessage("Moto is required.");
            RuleFor(r => r.StartDate).GreaterThan(DateTime.UtcNow).WithMessage("Start date must be in the future.");
            RuleFor(r => r.EndDate).GreaterThan(r => r.StartDate).WithMessage("End date must be after start date.");
        }
    }
}

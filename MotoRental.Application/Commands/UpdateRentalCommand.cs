using MediatR;
using MotoRental.Application.Exceptions;
using MotoRental.Domain.Entities;
using MotoRental.Domain.Enums;
using MotoRental.Infrastructure.Repositories;

namespace MotoRental.Application.Commands
{
    public class UpdateRentalCommand : IRequest<decimal>
    {
        public Guid RentalId { get; set; }
        public DateTime ActualReturnDate { get; set; }
    }

    public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, decimal>
    {
        private readonly IRentalRepository _rentalRepository;

        public UpdateRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<decimal> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(request.RentalId);

            if (rental == null)
                throw new NotFoundException("Rental not found.");

            if (request.ActualReturnDate < rental.ExpectedReturnDate)
            {
                var remainingDays = (rental.ExpectedReturnDate - request.ActualReturnDate).Days;
                var penaltyRate = rental.Plan switch
                {
                    RentalPlan.SevenDays => 0.2m,
                    RentalPlan.FifteenDays => 0.4m,
                    _ => 0m
                };
                rental.PenaltyCost = remainingDays * penaltyRate * rental.TotalCost / GetPlanDays(rental.Plan);
            }
            else if (request.ActualReturnDate > rental.ExpectedReturnDate)
            {
                var extraDays = (request.ActualReturnDate - rental.ExpectedReturnDate).Days;
                rental.PenaltyCost = extraDays * 50m;
            }

            rental.TotalCost += rental.PenaltyCost;

            await _rentalRepository.UpdateAsync(rental);
            return rental.TotalCost;
        }

        private int GetPlanDays(RentalPlan plan)
        {
            return plan switch
            {
                RentalPlan.SevenDays => 7,
                RentalPlan.FifteenDays => 15,
                RentalPlan.ThirtyDays => 30,
                RentalPlan.FortyFiveDays => 45,
                RentalPlan.FiftyDays => 50,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

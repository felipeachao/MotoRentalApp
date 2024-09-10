using MediatR;
using MotoRental.Domain.Entities;
using MotoRental.Domain.Enums;
using MotoRental.Infrastructure.Repositories;

namespace MotoRental.Application.Commands
{
    public class CreateRentalCommand : IRequest<Guid>
    {
        public Guid DeliveryPersonId { get; set; }
        public Guid MotoId { get; set; }
        public RentalPlan Plan { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Guid>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IDeliveryPersonRepository deliveryPersonRepository)
        {
            _rentalRepository = rentalRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
        }

        public async Task<Guid> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(request.DeliveryPersonId);
            if (deliveryPerson.CnhType != CnhType.A && deliveryPerson.CnhType != CnhType.A_B)
                throw new InvalidOperationException("Delivery person is not eligible to rent a moto.");

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                DeliveryPersonId = request.DeliveryPersonId,
                MotoId = request.MotoId,
                Plan = request.Plan,
                StartDate = request.StartDate.AddDays(1),
                EndDate = request.StartDate.AddDays(GetPlanDays(request.Plan)),
                ExpectedReturnDate = request.StartDate.AddDays(GetPlanDays(request.Plan)),
                TotalCost = CalculateRentalCost(request.Plan)
            };

            await _rentalRepository.AddAsync(rental);
            return rental.Id;
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

        private decimal CalculateRentalCost(RentalPlan plan)
        {
            return plan switch
            {
                RentalPlan.SevenDays => 7 * 30m,
                RentalPlan.FifteenDays => 15 * 28m,
                RentalPlan.ThirtyDays => 30 * 22m,
                RentalPlan.FortyFiveDays => 45 * 20m,
                RentalPlan.FiftyDays => 50 * 18m,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

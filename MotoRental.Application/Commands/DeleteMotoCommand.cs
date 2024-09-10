using MediatR;
using MotoRental.Infrastructure.Repositories;

namespace MotoRental.Application.Commands
{
    public class DeleteMotoCommand : IRequest<Unit>
    {
        public Guid MotoId { get; set; }
    }

    public class DeleteMotoCommandHandler : IRequestHandler<DeleteMotoCommand, Unit>
    {
        private readonly IMotoRepository _motoRepository;
        private readonly IRentalRepository _rentalRepository;

        public DeleteMotoCommandHandler(IMotoRepository motoRepository, IRentalRepository rentalRepository)
        {
            _motoRepository = motoRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<Unit> Handle(DeleteMotoCommand request, CancellationToken cancellationToken)
        {
            if (await _rentalRepository.HasRentalsAsync(request.MotoId))
            {
                throw new InvalidOperationException("Moto cannot be deleted because it has active rentals.");
            }

            await _motoRepository.DeleteAsync(request.MotoId);
            return Unit.Value;
        }
    }
}

using MediatR;
using MotoRental.Application.Exceptions;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Repositories;
using System;

namespace MotoRental.Application.Commands
{
    public class UpdateMotoLicensePlateCommand : IRequest<Unit>
    {
        public Guid MotoId { get; set; }
        public string NewLicensePlate { get; set; }
    }

    public class UpdateMotoLicensePlateCommandHandler : IRequestHandler<UpdateMotoLicensePlateCommand, Unit>
    {
        private readonly IMotoRepository _motoRepository;

        public UpdateMotoLicensePlateCommandHandler(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public async Task<Unit> Handle(UpdateMotoLicensePlateCommand request, CancellationToken cancellationToken)
        {
            var moto = await _motoRepository.GetByIdAsync(request.MotoId);
            if (moto == null)
                throw new NotFoundException("Moto not found.");

            moto.LicensePlate = request.NewLicensePlate;
            await _motoRepository.UpdateAsync(moto);

            return Unit.Value;
        }
    }
}

using MediatR;
using MotoRental.Application.Events;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Messaging;
using MotoRental.Infrastructure.Repositories;
using System;

namespace MotoRental.Application.Commands
{
    public class CreateMotoCommand : IRequest<Guid>
    {
        public required string Model { get; set; }
        public required string LicensePlate { get; set; }
        public int Year { get; set; }
    }

    public class CreateMotoCommandHandler(IMotoRepository repository, IMessagePublisher messagePublisher) : IRequestHandler<CreateMotoCommand, Guid>
    {
        private readonly IMotoRepository _repository = repository;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<Guid> Handle(CreateMotoCommand request, CancellationToken cancellationToken)
        {
            var moto = new Moto(request.Model, request.LicensePlate, request.Year)
            {
                Model = request.Model,
                LicensePlate = request.LicensePlate,
                Year = request.Year
            };

            await _repository.AddAsync(moto);

            var evento = new MotoRegisterEvent(moto.Model, moto.LicensePlate, moto.Year)
            {
                Year = moto.Year,
                Model = moto.Model,
                LicensePlate = moto.LicensePlate
            };
            await _messagePublisher.PublishAsync(evento);

            return moto.Id;
        }
    }
}

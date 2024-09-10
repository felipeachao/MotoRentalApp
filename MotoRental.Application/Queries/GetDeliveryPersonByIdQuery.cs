using MediatR;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Repositories;
using System;

namespace MotoRental.Application.Queries
{
    public class GetDeliveryPersonByIdQuery : IRequest<DeliveryPerson>
    {
        public Guid DeliveryPersonId { get; set; }
    }

    public class GetDeliveryPersonByIdQueryHandler(IDeliveryPersonRepository deliveryPersonRepository) : IRequestHandler<GetDeliveryPersonByIdQuery, DeliveryPerson>
    {
        private readonly IDeliveryPersonRepository _deliveryPersonRepository = deliveryPersonRepository;

        public async Task<DeliveryPerson> Handle(GetDeliveryPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryPersonRepository.GetByIdAsync(request.DeliveryPersonId);
        }
    }
}

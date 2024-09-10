using MediatR;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Repositories;
using System.Collections.Generic;

namespace MotoRental.Application.Queries
{
    public class GetAllDeliveryPersonsQuery : IRequest<IEnumerable<DeliveryPerson>>
    {
    }

    public class GetAllDeliveryPersonsQueryHandler(IDeliveryPersonRepository deliveryPersonRepository) : IRequestHandler<GetAllDeliveryPersonsQuery, IEnumerable<DeliveryPerson>>
    {
        private readonly IDeliveryPersonRepository _deliveryPersonRepository = deliveryPersonRepository;

        public async Task<IEnumerable<DeliveryPerson>> Handle(GetAllDeliveryPersonsQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryPersonRepository.GetAllAsync();
        }
    }
}

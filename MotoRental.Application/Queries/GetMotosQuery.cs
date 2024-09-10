using MediatR;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MotoRental.Application.Queries
{
    public class GetMotosQuery : IRequest<IEnumerable<Moto>>
    {
        public required string LicensePlate { get; set; }
    }

    public class GetMotosQueryHandler(IMotoRepository motoRepository) : IRequestHandler<GetMotosQuery, IEnumerable<Moto>>
    {
        private readonly IMotoRepository _motoRepository = motoRepository;

        public async Task<IEnumerable<Moto>> Handle(GetMotosQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.LicensePlate))
            {
                return await _motoRepository.GetAllAsync(request.LicensePlate);
            }
            return await _motoRepository.GetAllAsync();
        }
    }
}

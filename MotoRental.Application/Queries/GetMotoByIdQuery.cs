using MediatR;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Repositories;
using System;

namespace MotoRental.Application.Queries
{
    public class GetMotoByIdQuery : IRequest<Moto>
    {
        public Guid MotoId { get; set; }
    }

    public class GetMotoByIdQueryHandler(IMotoRepository motoRepository) : IRequestHandler<GetMotoByIdQuery, Moto>
    {
        private readonly IMotoRepository _motoRepository = motoRepository;

        public async Task<Moto> Handle(GetMotoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _motoRepository.GetByIdAsync(request.MotoId);
        }
    }
}


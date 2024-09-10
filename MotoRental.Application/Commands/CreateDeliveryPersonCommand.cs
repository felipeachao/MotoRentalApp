using MediatR;
using MotoRental.Domain.Entities;
using MotoRental.Infrastructure.Repositories; 

namespace MotoRental.Application.Commands
{
    public class CreateDeliveryPersonCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Cnpj { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string CnhNumber { get; set; }
        public CnhType CnhType { get; set; }
    }

   public class CreateDeliveryPersonCommandHandler : IRequestHandler<CreateDeliveryPersonCommand, Guid>
{
    private readonly IDeliveryPersonRepository _repository;

    public CreateDeliveryPersonCommandHandler(IDeliveryPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateDeliveryPersonCommand request, CancellationToken cancellationToken)
    {
        var deliveryPerson = new DeliveryPerson
            {
                Name = request.Name,
                Cnpj = request.Cnpj,
                DateOfBirth = request.DateOfBirth,
                CnhNumber = request.CnhNumber,
                CnhType = request.CnhType,
                CnhImagePath = "/path/to/cnh/image.png" 
            };


        await _repository.AddAsync(deliveryPerson);
        return deliveryPerson.Id;
    }
}

}

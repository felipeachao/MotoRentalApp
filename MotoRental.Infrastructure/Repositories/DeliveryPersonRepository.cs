using Microsoft.EntityFrameworkCore;

namespace MotoRental.Infrastructure.Repositories
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly MotoRentalDbContext _context;

        public DeliveryPersonRepository(MotoRentalDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryPerson> GetByIdAsync(Guid id)
        {
            return await _context.DeliveryPersons.FindAsync(id);
        }

        public async Task<IEnumerable<DeliveryPerson>> GetAllAsync()
        {
            return await _context.DeliveryPersons.ToListAsync();
        }

        public async Task AddAsync(DeliveryPerson deliveryPerson)
        {
            await _context.DeliveryPersons.AddAsync(deliveryPerson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DeliveryPerson deliveryPerson)
        {
            _context.DeliveryPersons.Update(deliveryPerson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var deliveryPerson = await GetByIdAsync(id);
            if (deliveryPerson != null)
            {
                _context.DeliveryPersons.Remove(deliveryPerson);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByCnpjAsync(string cnpj)
        {
            return await _context.DeliveryPersons.AnyAsync(d => d.Cnpj == cnpj);
        }

        public async Task<bool> ExistsByCnhNumberAsync(string cnhNumber)
        {
            return await _context.DeliveryPersons.AnyAsync(d => d.CnhNumber == cnhNumber);
        }
    }
}

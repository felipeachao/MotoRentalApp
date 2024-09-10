using Microsoft.EntityFrameworkCore;
using MotoRental.Domain.Entities;

namespace MotoRental.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly MotoRentalDbContext _context;

        public RentalRepository(MotoRentalDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> GetByIdAsync(Guid id)
        {
            return await _context.Rentals.FindAsync(id);
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var rental = await GetByIdAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> HasRentalsAsync(Guid motoId)
        {
            throw new NotImplementedException();
        }
    }
}

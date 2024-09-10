using Microsoft.EntityFrameworkCore;
using MotoRental.Domain.Entities;

namespace MotoRental.Infrastructure.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly MotoRentalDbContext _context;

        public MotoRepository(MotoRentalDbContext context)
        {
            _context = context;
        }

        public async Task<Moto> GetByIdAsync(Guid id)
        {
            return await _context.Motos.FindAsync(id);
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            return await _context.Motos.ToListAsync();
        }

        public async Task AddAsync(Moto moto)
        {
            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Moto moto)
        {
            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var moto = await GetByIdAsync(id);
            if (moto != null)
            {
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Moto>> GetAllAsync(string licensePlate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LicensePlateExistsAsync(string licensePlate)
        {
            throw new NotImplementedException();
        }
    }
}

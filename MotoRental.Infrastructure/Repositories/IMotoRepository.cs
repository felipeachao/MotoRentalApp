using MotoRental.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotoRental.Infrastructure.Repositories
{
    public interface IMotoRepository
    {
        Task AddAsync(Moto moto);
        Task<Moto> GetByIdAsync(Guid id);
        Task<IEnumerable<Moto>> GetAllAsync();  
        Task<IEnumerable<Moto>> GetAllAsync(string licensePlate);  
        Task UpdateAsync(Moto moto);
        Task DeleteAsync(Guid id);
        Task<bool> LicensePlateExistsAsync(string licensePlate);
    }
}

using MotoRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotoRental.Infrastructure.Repositories
{
    public interface IRentalRepository
    {
        Task AddAsync(Rental rental);
        Task<Rental> GetByIdAsync(Guid id);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task UpdateAsync(Rental rental);
        Task<bool> HasRentalsAsync(Guid motoId);
    }
}

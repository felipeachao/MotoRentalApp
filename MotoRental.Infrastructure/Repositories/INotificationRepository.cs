using MotoRental.Domain.Entities;
using System.Threading.Tasks;

namespace MotoRental.Infrastructure.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<Notification> GetByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetAllAsync();
    }
}

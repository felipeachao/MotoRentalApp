using Microsoft.EntityFrameworkCore;
using MotoRental.Domain.Entities;

namespace MotoRental.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MotoRentalDbContext _context;

        public NotificationRepository(MotoRentalDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Notification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public Task<Notification> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

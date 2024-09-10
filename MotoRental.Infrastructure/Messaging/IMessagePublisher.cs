using System.Threading.Tasks;

namespace MotoRental.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message);
    }
}

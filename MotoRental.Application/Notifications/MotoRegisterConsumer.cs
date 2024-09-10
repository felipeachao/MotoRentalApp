using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MotoRental.Application.Events;
using MotoRental.Infrastructure.Repositories;
using MotoRental.Domain.Entities;

namespace MotoRental.Application.Consumers
{
    public class MotoRegisterConsumer
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MotoRegisterConsumer(INotificationRepository notificationRepository, IConnection connection)
        {
            _notificationRepository = notificationRepository;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connection;
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "moto_queue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                var motoEvent = JsonSerializer.Deserialize<MotoRegisterEvent>(message);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (motoEvent.Year == 2024)
                {
                    var notification = new Notification
                    {
                        Id = Guid.NewGuid(),
                        Event = $"Moto cadastrada para o ano 2024: Modelo {motoEvent.Model}, Placa {motoEvent.LicensePlate}",
                        CreatedAt = DateTime.UtcNow
                    };

                    await _notificationRepository.AddAsync(notification);
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            };
            _channel.BasicConsume(queue: "moto_queue",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}

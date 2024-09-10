using RabbitMQ.Client;
using System;
using System.Text;

public class RabbitMqClient
{
    private readonly IConnectionFactory _connectionFactory;

    public RabbitMqClient(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void SendMessage(string message)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine("Mensagem enviada: {0}", message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar a mensagem: {ex.Message}");
        }
    }
}

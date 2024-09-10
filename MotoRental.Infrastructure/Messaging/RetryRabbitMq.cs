using Polly;
using RabbitMQ.Client;
using System;

public class RabbitMqConnectionManager
{
    private readonly IConnectionFactory _connectionFactory;

    public RabbitMqConnectionManager(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IConnection GetRabbitMqConnection()
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(5), 
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Tentativa {retryCount} falhou. Erro: {exception.Message}. Tentando novamente em {timeSpan.TotalSeconds} segundos.");
                });

        return retryPolicy.Execute(() =>
        {
            Console.WriteLine("Tentando conectar ao RabbitMQ...");
            return _connectionFactory.CreateConnection();
        });
    }
}

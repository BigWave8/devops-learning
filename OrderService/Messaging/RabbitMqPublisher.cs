using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace OrderService.Messaging
{
    public class RabbitMqPublisher : IEventPublisher, IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;

        public RabbitMqPublisher()
        {
            _factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        private async Task<IConnection> GetConnectionAsync()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = await _factory.CreateConnectionAsync();
            }

            return _connection;
        }

        public async Task PublishAsync<T>(T @event, string queueName) where T : class
        {
            var connection = await GetConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false);

            var json = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: queueName,
                                            body: body);
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection is { IsOpen: true })
            {
                await Task.Run(() => _connection.Dispose());
            }
        }
    }
}

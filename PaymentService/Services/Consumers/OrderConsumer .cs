/*using PaymentService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using Contracts.Events;

namespace PaymentService.Services.Consumers
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection? _connection;
        private IChannel? _channel;

        public OrderConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeConnectionAsync();
            await StartConsumingAsync(stoppingToken);
        }

        private async Task InitializeConnectionAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(
                queue: "order-created",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        private async Task StartConsumingAsync(CancellationToken cancellationToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                    if (order != null)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var processor = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                        var result = await processor.ProcessAsync(new PaymentRequestDto
                        {
                            OrderId = order.OrderId,
                            UserId = order.UserId,
                            BookId = order.BookId,
                            Price = order.Price
                        });
                    }

                    await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    await _channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: "order-created", 
                autoAck: false, 
                consumer: consumer, 
                cancellationToken);

            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // stop service
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
                await _channel.DisposeAsync();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }

            await base.StopAsync(cancellationToken);
        }
    }
}
*/
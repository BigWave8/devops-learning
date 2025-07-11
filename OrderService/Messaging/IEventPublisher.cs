namespace OrderService.Messaging
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event, string queueName) where T : class;
    }
}

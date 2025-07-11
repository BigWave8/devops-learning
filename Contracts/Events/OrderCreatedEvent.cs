namespace Contracts.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

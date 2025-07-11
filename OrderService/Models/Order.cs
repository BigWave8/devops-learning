namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

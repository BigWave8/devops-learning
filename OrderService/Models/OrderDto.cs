namespace OrderService.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace OrderService.Models
{
    public class PaymentRequestDto
    {
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public double Price { get; set; }
    }
}

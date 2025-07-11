namespace PaymentService.Models
{
    public class PaymentRequestDto
    {
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public decimal Price { get; set; }
    }
}

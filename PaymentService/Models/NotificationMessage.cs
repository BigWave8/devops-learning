namespace PaymentService.Models
{
    public class NotificationMessage
    {
        public int OrderId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
    }
}

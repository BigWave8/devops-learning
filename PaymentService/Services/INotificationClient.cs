using PaymentService.Models;

namespace PaymentService.Services
{
    public interface INotificationClient
    {
        Task SendNotificationAsync(NotificationMessage message);
    }
}

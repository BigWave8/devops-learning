using PaymentService.Models;

namespace PaymentService.Services
{
    public class NotificationClient : INotificationClient
    {
        private readonly HttpClient _http;

        public NotificationClient(HttpClient http)
        {
            _http = http;
        }

        public async Task SendNotificationAsync(NotificationMessage message)
        {
            await _http.PostAsJsonAsync("api/notifications", message);
        }
    }
}

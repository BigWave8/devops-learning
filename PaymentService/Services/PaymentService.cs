using Contracts.Protos;
using PaymentService.Models;
using System.Text.Json;
using System.Text;

namespace PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUserClient _userClient;
        private readonly INotificationClient _notificationClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentService(
            IUserClient userClient, 
            INotificationClient notificationClient,
            IHttpClientFactory httpClientFactory)
        {
            _userClient = userClient;
            _notificationClient = notificationClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaymentResponseDto> ProcessAsync(PaymentRequestDto request)
        {
            /*var user = await _userClient.GetUserAsync(request.UserId);
            if (user == null)
            {
                return new PaymentResponse { Success = false, Message = "User not found" };
            }*/

            //user.Balance -= request.Price;

            /*
            BookResponse book;
            try
            {
                book = await _bookClient.GetBookByIdAsync(new BookRequest { Id = request.BookId });
            }
            catch
            {
                return new PaymentResponse { Success = false, Message = "Book not found." };
            }
            */
            var orderPayload = new
            {
                request.OrderId,
                request.UserId,
                request.BookId,
                request.Price
            };
            /*
            var httpClient = _httpClientFactory.CreateClient("OrderService");
            var content = new StringContent(JsonSerializer.Serialize(orderPayload), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/orders", content);
            if (!response.IsSuccessStatusCode)
            {
                return new PaymentResponse { Success = false, Message = "Failed to create order." };
            }*/

            /*await _notificationClient.SendNotificationAsync(new NotificationMessage
            {
                OrderId = request.OrderId,
                ToUserId = request.UserId,
                Message = $"Ваш платіж на суму {request.Price} грн пройшов успішно"
            });
            
             
             
             var notification = new NotificationMessage
            {
                ToUserId = request.UserId,
                Message = $"Ваш платіж на суму {request.Amount} грн пройшов успішно"
            };

            await _publisher.PublishAsync(notification, "notification-created");*/

            return new PaymentResponseDto { Success = true, Message = "Payment and order created." };
        }
    }
}

using Azure;
using Azure.Core;
using Contracts.Events;
using Contracts.Protos;
using OrderService.Messaging;
using OrderService.Models;
using OrderService.Repositories;
using System.Net.Http;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly BookProtoService.BookProtoServiceClient _bookClient;
        private readonly IOrderRepository _orderRepository;
        private readonly IEventPublisher _publisher;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderService(
            BookProtoService.BookProtoServiceClient bookClient,
            IOrderRepository orderRepository, 
            IEventPublisher publisher,
            IHttpClientFactory httpClientFactory)
        {
            _bookClient = bookClient;
            _orderRepository = orderRepository;
            _publisher = publisher;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            BookResponse book;
            try
            {
                book = await _bookClient.GetBookByIdAsync(new BookRequest { Id = dto.BookId });
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't find book with id {dto.BookId} because of: {ex.Message}");
            }

            var order = new Order
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                BookTitle = book.Title,
                Price = book.Price
            };

            await _orderRepository.AddAsync(order);

            var orderEvent = new OrderCreatedEvent
            {
                OrderId = order.Id,
                UserId = order.UserId,
                BookId = order.BookId,
                Price = order.Price
            };

            //await _publisher.PublishAsync(orderEvent, "order-created");

            var client = _httpClientFactory.CreateClient("PaymentService");
            var paymentRequest = new PaymentRequestDto
            {
                OrderId = order.Id,
                UserId = dto.UserId,
                BookId = dto.BookId,
                Price = order.Price
            };

            var response = await client.PostAsJsonAsync("api/payment", paymentRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("The payment has not been processed.");
            }

            return new OrderDto
            {
                OrderId = order.Id,
                UserId = order.UserId,
                BookId = order.BookId,
                BookTitle = order.BookTitle,
                Price = order.Price,
                CreatedAt = order.CreatedAt
            };
        }
    }
}

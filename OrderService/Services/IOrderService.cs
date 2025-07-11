using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    }
}

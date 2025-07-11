using Contracts.Protos;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    //private readonly BookProtoService.BookProtoServiceClient _bookClient;
    private readonly IOrderService _orderService;

    public OrdersController(
        //BookProtoService.BookProtoServiceClient bookClient,
        IOrderService orderService)
    {
        //_bookClient = bookClient;
        _orderService = orderService;
    }

    /*[HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        try
        {
            var result = await _bookClient.GetBookByIdAsync(new BookRequest { Id = id });
            return Ok(result);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            return NotFound(ex.Status.Detail);
        }
    }*/

    /*[HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var result = await _bookClient.GetAllBooksAsync(new Empty());
        return Ok(result.Books);
    }*/

    /*[HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderDto dto)
    {
        // Тут могла би бути реальна логіка збереження замовлення, а поки просто лог.
        Console.WriteLine($"Creating order {dto.OrderId} for user {dto.UserId} and book '{dto.BookTitle}' for {dto.Price} грн");

        return Ok(new { Message = "Order created successfully" });
    }*/

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order = await _orderService.CreateOrderAsync(dto);

        return Ok(order);
    }
}
using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessAsync(PaymentRequestDto request);
    }
}

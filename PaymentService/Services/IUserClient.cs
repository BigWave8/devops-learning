using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IUserClient
    {
        Task<UserDto?> GetUserAsync(int userId);
    }
}

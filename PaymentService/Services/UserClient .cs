using PaymentService.Models;

namespace PaymentService.Services
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _http;

        public UserClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserDto?> GetUserAsync(int userId)
        {
            return await _http.GetFromJsonAsync<UserDto>($"api/users/{userId}");
        }
    }
}

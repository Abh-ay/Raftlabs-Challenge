using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Handlers;
using UserService.Misc;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await ExceptionHandler.ExecuteWithHandlingAsync<User>(async () =>
            {
                _logger.LogDebug("**API request Sttarted**");
                var response = await _httpClient.GetAsync($"users/{userId}");
                _logger.LogDebug("**API request Ended **");
               
                if (!response.IsSuccessStatusCode)
                    throw new NotFoundException($"User not found with {userId}");

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<SingleUserResponse<User>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (user == null || user.Data == null)
                {
                    throw new ApiException("Invalid response structure or missing user data", HttpStatusCode.InternalServerError);
                }
                _logger.LogInformation($"User is fetched successfully with {userId}");
                return user.Data;
            }, _logger, $"******GetUserByIdAsync({userId})******");
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await ExceptionHandler.ExecuteWithHandlingAsync<IEnumerable<User>>(async () =>
            {
                var users = new List<User>();
                int page = 1;
                bool hasMorePages = false;

                do
                {
                    var response = await _httpClient.GetAsync($"users?page={page}");
                    if (!response.IsSuccessStatusCode)
                        throw new ApiException("Failed to fetch users", response.StatusCode);

                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<PagerResponse<User>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (result is null || result.Data is null)
                        throw new ApiException("Invalid API response", HttpStatusCode.InternalServerError);

                    users.AddRange(result.Data);
                    hasMorePages = page < result.Total_Pages;
                    page++;

                } while (hasMorePages);

                return users;

            }, _logger, "****GetAllUsersAsync()*****");
        }
    }

}

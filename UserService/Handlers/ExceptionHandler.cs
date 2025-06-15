
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using UserService.Misc;

namespace UserService.Handlers
{
    public static class ExceptionHandler
    {
        public static async Task<T> ExecuteWithHandlingAsync<T>(Func<Task<T>> action, ILogger logger, string operation)
        {
            try
            {
                return await action();
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (ApiException ex)
            {
                logger.LogError(ex, $"API error occurred during {operation}");
                throw;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, $"Network error occurred during {operation}");
                throw new ApiException("Network error", HttpStatusCode.ServiceUnavailable);
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"Json Serilization Error Occured {operation}");
                throw new ApiException("Serilization error", HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                logger.LogError(ex, $"Unexpected error during {operation}");
                throw new ApiException("Unexpected error occurred", HttpStatusCode.InternalServerError);
            }
        }
    }

}

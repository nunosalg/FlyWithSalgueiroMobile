using FlyWithSalgueiroMobile.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace FlyWithSalgueiroMobile.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;
        private readonly string _baseUrl = "https://lzl75nls-44301.uks1.devtunnels.ms/";

        JsonSerializerOptions _serializerOptions;
        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<ApiResponse<bool>> RegisterUser(string firstName, string lastName, string email, DateTime birthDate, string password, string confirmPassword)
        {
            try
            {
                var register = new Register()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    BirthDate = birthDate,
                    Password = password,
                    Confirm = confirmPassword
                };

                var json = JsonSerializer.Serialize(register, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await PostRequest("api/Customers/Register", content);

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Detailed error: {errorContent}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error sending HTTP requisition: {response.StatusCode}");
                    return new ApiResponse<bool>
                    {
                        ErrorMessage = $"Error sending HTTP requisition: {response.StatusCode}"
                    };
                }

                return new ApiResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering user: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<bool>> Login(string email, string password)
        {
            try
            {
                var login = new Login()
                {
                    Email = email,
                    Password = password
                };

                var json = JsonSerializer.Serialize(login, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await PostRequest("api/Customers/Login", content);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error sending HTTP requisition: {response.StatusCode}");
                    return new ApiResponse<bool>
                    {
                        ErrorMessage = $"Error sending HTTP requisition: {response.StatusCode}"
                    };
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Token>(jsonResult, _serializerOptions);

                Preferences.Set("accesstoken", result!.AccessToken);
                Preferences.Set("userid", result.UserId!);
                Preferences.Set("username", result.UserName);

                return new ApiResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during login : {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }
        }

        private async Task<HttpResponseMessage> PostRequest(string uri, HttpContent content)
        {
            var urlAddress = _baseUrl + uri;
            try
            {
                var result = await _httpClient.PostAsync(urlAddress, content);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending POST requisition to {uri}: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}

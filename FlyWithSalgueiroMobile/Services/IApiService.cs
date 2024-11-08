using FlyWithSalgueiroMobile.Models;

namespace FlyWithSalgueiroMobile.Services
{
    public interface IApiService
    {
        Task<ApiResponse<bool>> RegisterUser(string firstName, string lastName, string email, DateTime birthDate, string password, string confirmPassword);

        Task<ApiResponse<bool>> Login(string email, string password);

        Task<ApiResponse<bool>> RecoverPassword(string email);

        Task<ApiResponse<bool>> ResetPassword(string email, string token, string password, string confirm);

        Task<ApiResponse<bool>> UploadUserImage(byte[] imageArray);

        Task<ApiResponse<bool>> UpdateUserInfo(string firstName, string lastName, DateTime birthDate);

        Task<(ProfileImage? ProfileImage, string? ErrorMessage)> GetUserProfileImage();

        Task<(IEnumerable<City>? Cities, string? ErrorMessage)> GetCities();

        Task<(IEnumerable<Flight>? SearchedFlights, string? ErrorMessage)> GetSearchedFlights(int? originId, int? destinationId, DateTime? departure);

        Task<(UserInfo? UserInfo, string? ErrorMessage)> GetUserInfo();
    }
}

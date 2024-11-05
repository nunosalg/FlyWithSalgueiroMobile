namespace FlyWithSalgueiroMobile.Services
{
    public interface IApiService
    {
        Task<ApiResponse<bool>> RegisterUser(string firstName, string lastName, string email, DateTime birthDate, string password, string confirmPassword);

        Task<ApiResponse<bool>> Login(string email, string password);

        Task<ApiResponse<bool>> RecoverPassword(string email);

        Task<ApiResponse<bool>> ResetPassword(string email, string token, string password, string confirm);
    }
}

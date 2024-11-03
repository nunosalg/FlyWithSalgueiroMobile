namespace FlyWithSalgueiroMobile.Services
{
    public interface IApiService
    {
        Task<ApiResponse<bool>> RegisterUser(string firstName, string lastName, string email, DateTime birthDate, string password, string confirmPassword);

        Task<ApiResponse<bool>> Login(string email, string password);

    }
}

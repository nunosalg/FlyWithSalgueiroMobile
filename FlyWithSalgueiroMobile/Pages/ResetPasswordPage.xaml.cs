using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile.Pages;

public partial class ResetPasswordPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IValidator _validator;
    private readonly string _email;

    public ResetPasswordPage(IApiService apiService, IValidator validator, string email)
	{
		InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        _email = email;
    }

    private async void BtnResetPassword_Clicked(object sender, EventArgs e)
    {
        LoadingIndicator.IsVisible = true;
        BtnResetPassword.IsEnabled = false;

        var token = EntToken.Text;

        if (string.IsNullOrEmpty(token))
        {
            await DisplayAlert("Error", "Type your token", "Cancel");
            return;
        }

        var password = EntPassword.Text;

        if (string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Type your password", "Cancel");
            return;
        }

        var confirm = EntConfirmPassword.Text;

        if (string.IsNullOrEmpty(confirm))
        {
            await DisplayAlert("Error", "Confirm your password", "Cancel");
            return;
        }

        if (password.Length < 6)
        {
            await DisplayAlert("Error", "Password must contain at least 6 characters", "Cancel");
            return;
        }

        if (confirm != password)
        {
            await DisplayAlert("Error", "Confirm doesn't match with password", "Cancel");
            return;
        }

        var response = await _apiService.ResetPassword(_email, token, password, confirm);

        if (!response.HasError)
        {
            await DisplayAlert("Reset", "Password reset successfully!", "Ok");
            await Navigation.PushAsync(new LoginPage(_apiService, _validator));
        }
        else
        {
            await DisplayAlert("Error", "Something went wrong", "Cancel");
        }

        LoadingIndicator.IsVisible = false;
        BtnResetPassword.IsEnabled = true;
    }

    private async void TapBackToLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}
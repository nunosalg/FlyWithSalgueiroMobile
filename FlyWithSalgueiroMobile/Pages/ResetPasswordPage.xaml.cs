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

    private async void TapBackToLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }

    private async void TapReset_Tapped(object sender, TappedEventArgs e)
    {
        LoadingIndicator.IsVisible = true;
        FrameReset.IsEnabled = false;

        var token = EntToken.Text;

        if (string.IsNullOrEmpty(token))
        {
            LoadingIndicator.IsVisible = false;
            FrameReset.IsEnabled = true;
            await DisplayAlert("Error", "Type your token", "Cancel");
            return;
        }

        var password = EntPassword.Text;

        if (string.IsNullOrEmpty(password))
        {
            LoadingIndicator.IsVisible = false;
            FrameReset.IsEnabled = true;
            await DisplayAlert("Error", "Type your password", "Cancel");
            return;
        }

        var confirm = EntConfirmPassword.Text;

        if (string.IsNullOrEmpty(confirm))
        {
            LoadingIndicator.IsVisible = false;
            FrameReset.IsEnabled = true;
            await DisplayAlert("Error", "Confirm your password", "Cancel");
            return;
        }

        if (password.Length < 6)
        {
            LoadingIndicator.IsVisible = false;
            FrameReset.IsEnabled = true;
            await DisplayAlert("Error", "Password must contain at least 6 characters", "Cancel");
            return;
        }

        if (confirm != password)
        {
            LoadingIndicator.IsVisible = false;
            FrameReset.IsEnabled = true;
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
        FrameReset.IsEnabled = true;
    }
}
using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IValidator _validator;

    public LoginPage(IApiService apiService, IValidator validator)
	{
		InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        LoadingIndicator.IsVisible = true;
        FrameLogin.IsEnabled = false;

        if (string.IsNullOrEmpty(EntEmail.Text))
        {
            LoadingIndicator.IsVisible = false;
            FrameLogin.IsEnabled = true;
            await DisplayAlert("Error", "Type your email", "Cancel");
            return;
        }

        if (string.IsNullOrEmpty(EntPassword.Text))
        {
            LoadingIndicator.IsVisible = false;
            FrameLogin.IsEnabled = true;
            await DisplayAlert("Error", "Type your password", "Cancel");
            return;
        }

        var response = await _apiService.Login(EntEmail.Text, EntPassword.Text);

        if (!response.HasError)
        {
            Application.Current!.MainPage = new AppShell(_apiService, _validator);
        }
        else
        {
            await DisplayAlert("Error", "Something went wrong", "Cancel");
        }

        LoadingIndicator.IsVisible = false;
        FrameLogin.IsEnabled = true;
    }

    private async void TapRegister_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage(_apiService, _validator));
    }

    private async void TapForgotPassword_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RecoverPasswordPage(_apiService, _validator));
    }
}
using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IValidator _validator;
    private int _flightId;

    public LoginPage(IApiService apiService, IValidator validator, int flightId = 0)
	{
		InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        _flightId = flightId;
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

            await Task.Delay(100);

            if (_flightId > 0)
            {
                Preferences.Set("SelectedFlightId", _flightId);

                await Shell.Current.GoToAsync(nameof(BuyTicketPage));
            }
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

    private async void TapBackHomepage_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new HomePage(_apiService, _validator));
    }
}
using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IValidator _validator;

    public RegisterPage(IApiService apiService, IValidator validator)
	{
		InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        DateOfBirthPicker.MaximumDate = DateTime.Today;
    }

    private async void BtnSignup_Clicked(object sender, EventArgs e)
    {
        if (await _validator.Validate(EntFirstName.Text, EntLastName.Text, EntEmail.Text, DateOfBirthPicker.Date, EntPassword.Text, EntConfirmPassword.Text))
        {
            var response = await _apiService.RegisterUser(EntFirstName.Text, EntLastName.Text, EntEmail.Text, DateOfBirthPicker.Date, EntPassword.Text, EntConfirmPassword.Text);

            if (!response.HasError)
            {
                await DisplayAlert("Register", "Account created successfully! Check your email to confirm!", "OK");
                await Navigation.PushAsync(new LoginPage(_apiService, _validator));
            }
            else
            {
                await DisplayAlert("Error", "Something went wrong!!!", "Cancel");
            }
        }
        else
        {
            string errorMessage = "";
            errorMessage += _validator.FirstNameError != null ? $"\n- {_validator.FirstNameError}" : "";
            errorMessage += _validator.LastNameError != null ? $"\n- {_validator.LastNameError}" : "";
            errorMessage += _validator.EmailError != null ? $"\n- {_validator.EmailError}" : "";
            errorMessage += _validator.BirthDateError != null ? $"\n- {_validator.BirthDateError}" : "";
            errorMessage += _validator.PasswordError != null ? $"\n- {_validator.PasswordError}" : "";
            errorMessage += _validator.ConfirmPasswordError != null ? $"\n- {_validator.ConfirmPasswordError}" : "";

            await DisplayAlert("Error", errorMessage, "OK");
        }
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}
using FlyWithSalgueiroMobile.Services;

namespace FlyWithSalgueiroMobile.Pages;

public partial class FutureFlightsPage : ContentPage
{
    private readonly IApiService _apiService;

    public FutureFlightsPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var (response, errorMessage) = await _apiService.GetFutureFlights();
        if (response == null)
        {
            await DisplayAlert("Error", "No future flights found", "Ok");
            return;
        }

        FutureFlights.ItemsSource = response;
    }
}
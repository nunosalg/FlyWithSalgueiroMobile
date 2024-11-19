using FlyWithSalgueiroMobile.Services;

namespace FlyWithSalgueiroMobile.Pages;

public partial class FlightsHistoryPage : ContentPage
{
    private readonly IApiService _apiService;

    public FlightsHistoryPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
	{
		base.OnAppearing();

        var (response, errorMessage) = await _apiService.GetFlightsHistory();
        if (response == null)
        {
            await DisplayAlert("Info", "No flights history found", "Ok");
            return;
        }

        FlightsHistory.ItemsSource = response;
    }
}
using FlyWithSalgueiroMobile.Models;
using FlyWithSalgueiroMobile.Services;

namespace FlyWithSalgueiroMobile.Pages;

public partial class HomePage : ContentPage
{
    private readonly IApiService _apiService;
    private IEnumerable<City>? _cities = new List<City>();

    public HomePage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;

        datePicker.MinimumDate = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var (response, errorMessage) = await _apiService.GetCities();
        if (response == null)
        {
            await DisplayAlert("Error", "Couldn't load cities", "Ok");
            return;
        }

        _cities = response;

        var originCities = new List<City>(_cities);
        originCities.Insert(0, new City { Id = 0, Name = "--------" });
        originPicker.ItemsSource = originCities;

        var destinationCities = new List<City>(_cities);
        destinationCities.Insert(0, new City { Id = 0, Name = "--------" });
        destinationPicker.ItemsSource = destinationCities;
    }

    private async void TapSearch_Tapped(object sender, TappedEventArgs e)
    {
        var selectedOrigin = originPicker.SelectedItem as City;
        var selectedDestination = destinationPicker.SelectedItem as City;

        if(selectedOrigin != null && selectedDestination != null && selectedOrigin.Name == selectedDestination.Name)
        {
            await DisplayAlert("Error", "Origin and destination must be different", "Ok");
            return;
        }

        DateTime? selectedDate = null;
        if (dateCheckbox.IsChecked)
        {
            selectedDate = datePicker.Date; 
        }

        if((selectedOrigin == null || selectedOrigin.Id < 1) && (selectedDestination == null || selectedDestination.Id < 1) && selectedDate == null)
        {
            await DisplayAlert("Error", "Choose the origin, destination or flight date", "Ok");
            return;
        } 
        
        if(selectedOrigin?.Id == 0)
        {
            selectedOrigin = null;
        }

        if(selectedDestination?.Id == 0)
        {
            selectedDestination = null;
        }

        var results = await _apiService.GetSearchedFlights(selectedOrigin?.Id, selectedDestination?.Id, selectedDate);
        if (results.SearchedFlights?.Any() == true)
        {
            flightResults.ItemsSource = results.SearchedFlights;
            flightResults.IsVisible = true;
        }
        else
        {
            await DisplayAlert("No Flights", "No flights found for the selected criteria.", "Ok");
            flightResults.IsVisible = false;
        }
    }

    private void TapBuyTicket_Tapped(object sender, TappedEventArgs e)
    {
        if(e.Parameter is int flightId)
        {
            Navigation.PushAsync(new BuyTicketPage(_apiService, flightId));
        }
    }
}
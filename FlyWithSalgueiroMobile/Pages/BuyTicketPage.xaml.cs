using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile.Pages;

public partial class BuyTicketPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IValidator _validator;
    private int _flightId;
    private decimal _ticketPrice;
    private IEnumerable<string>? _availableSeats = new List<string>();

    public BuyTicketPage()
    {
        InitializeComponent();

        _apiService = (IApiService)Application.Current.Handler.MauiContext.Services.GetService(typeof(IApiService));
        _validator = (IValidator)Application.Current.Handler.MauiContext.Services.GetService(typeof(IValidator));

        _flightId = Preferences.Get("SelectedFlightId", 0);
        Preferences.Remove("SelectedFlightId");
    }

    public BuyTicketPage(IApiService apiService, IValidator validator, int flightId)
	{
		InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        _flightId = flightId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        DateOfBirthPicker.MaximumDate = DateTime.Today;

        var (responseAvailableSeats, errorMessageAvailableSeats) = await _apiService.GetAvailableSeats(_flightId);
        if (responseAvailableSeats == null)
        {
            await DisplayAlert("Error", "Couldn't load available seats", "Ok");
            return;
        }

        _availableSeats = responseAvailableSeats;

        AvailableSeatsPicker.ItemsSource = (System.Collections.IList)_availableSeats;

        var (responseTicketPrice, errorMessageTicketPrice) = await _apiService.GetTicketPrice(_flightId);
        if (responseTicketPrice <= 0)
        {
            await DisplayAlert("Error", "Couldn't load ticket price", "Ok");
            return;
        }

        _ticketPrice = responseTicketPrice;
        EntPrice.Text = string.Format("{0:N2} €", _ticketPrice);
    }

    private async void TapBuyTicket_Tapped(object sender, TappedEventArgs e)
    {
        if (string.IsNullOrEmpty(EntFirstName.Text))
        {
            await DisplayAlert("Error", "Enter passenger's first name ", "Cancel");
            return;
        }

        if (string.IsNullOrEmpty(EntLastName.Text))
        {
            await DisplayAlert("Error", "Enter passenger's last name ", "Cancel");
            return;
        }

        if (string.IsNullOrEmpty(EntId.Text))
        {
            await DisplayAlert("Error", "Enter passenger's ID number ", "Cancel");
            return;
        }

        var passengerName = $"{EntFirstName.Text} {EntLastName.Text}";
        var passengerId = EntId.Text;
        var birthDate = DateOfBirthPicker.Date;

        if (birthDate.AddDays(365) > DateTime.Now)
        {
            await DisplayAlert("Error", "Passenger must be at least 1 year old ", "Cancel");
            return;
        }

        var seat = AvailableSeatsPicker.SelectedItem.ToString();

        LoadingIndicator.IsVisible = true;
        FrameBuyTicket.IsEnabled = false;

        var response = await _apiService.BuyTicket(_flightId, seat, passengerName, passengerId, birthDate, _ticketPrice);
        if (!response.HasError)
        {
            await DisplayAlert("Info", "Ticket bought successfully!", "Ok");
            await Navigation.PushAsync(new HomePage(_apiService, _validator));
        }
        else
        {
            await DisplayAlert("Error", "Something went wrong", "Cancel");
        }

        LoadingIndicator.IsVisible = false;
        FrameBuyTicket.IsEnabled = true;
    }

    private async void TapBackHomepage_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new HomePage(_apiService, _validator));
    }
}
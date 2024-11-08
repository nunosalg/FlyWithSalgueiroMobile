using FlyWithSalgueiroMobile.Services;

namespace FlyWithSalgueiroMobile.Pages;

public partial class BuyTicketPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly int _flightId;

    public BuyTicketPage(IApiService apiService, int flightId)
	{
		InitializeComponent();
        _apiService = apiService;
        _flightId = flightId;
    }
}
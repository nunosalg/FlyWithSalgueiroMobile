using FlyWithSalgueiroMobile.Pages;
using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile
{
    public partial class AppShell : Shell
    {
        private readonly IApiService _apiService;
        private readonly IValidator _validator;

        public AppShell(IApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService;
            _validator = validator;

            Routing.RegisterRoute(nameof(BuyTicketPage), typeof(BuyTicketPage));

            ConfigureShell();
        }

        private void ConfigureShell()
        {
            var homePage = new SearchFlightsPage(_apiService, _validator);
            var futureFlightsPage = new FutureFlightsPage(_apiService);
            var flightsHistoryPage = new FlightsHistoryPage(_apiService);
            var accountPage = new AccountPage(_apiService, _validator);
            var aboutPage = new AboutPage();

            Items.Add(new TabBar
            {
                Items =
                {
                    new ShellContent { Title = "Home",Icon = "home",Content = homePage  },
                    new ShellContent { Title = "My Flights",Icon = "futureflights",Content = futureFlightsPage },
                    new ShellContent { Title = "History",Icon = "flightshistory",Content = flightsHistoryPage },
                    new ShellContent { Title = "Account",Icon = "account",Content = accountPage },
                    new ShellContent { Title = "About",Icon = "about",Content = aboutPage }
                }
            });
        }
    }
}

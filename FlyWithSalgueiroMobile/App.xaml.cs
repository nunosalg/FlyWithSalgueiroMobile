using FlyWithSalgueiroMobile.Pages;
using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile
{
    public partial class App : Application
    {
        private readonly IApiService _apiService;
        private readonly IValidator _validator;

        public App(IApiService apiService, IValidator validator)
        {
            InitializeComponent();

            _apiService = apiService;
            _validator = validator;

            MainPage = new NavigationPage(new RegisterPage(apiService, validator));

        }
    }
}

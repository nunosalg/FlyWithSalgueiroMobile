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
        }
    }
}

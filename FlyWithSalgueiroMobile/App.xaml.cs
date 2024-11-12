﻿using FlyWithSalgueiroMobile.Pages;
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

            SetMainPage();
        }

        private void SetMainPage()
        {
            var accessToken = Preferences.Get("accesstoken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
                MainPage = new NavigationPage(new LoginPage(_apiService, _validator));
                return;
            }
            else
            {
                MainPage = new AppShell(_apiService, _validator);
            }
        }
    }
}

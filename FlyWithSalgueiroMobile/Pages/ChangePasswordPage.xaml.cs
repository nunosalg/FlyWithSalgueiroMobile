using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;

namespace FlyWithSalgueiroMobile.Pages;

public partial class ChangePasswordPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IValidator _validator;

    public ChangePasswordPage(IApiService apiService, IValidator validator)
	{
		InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }
}
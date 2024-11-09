namespace FlyWithSalgueiroMobile.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async void ImgBtnLinkedIn_Clicked(object sender, EventArgs e)
    {
        var linkedinUrl = "https://www.linkedin.com/in/nuno-salgueiro-9b3a7810b/";

        // Checks if it's a valid URL and if it can be opened
        if (Uri.TryCreate(linkedinUrl, UriKind.Absolute, out var uri))
        {
            await Launcher.OpenAsync(uri);
        }
        else
        {
            await DisplayAlert("Error", "Unable to open LinkedIn page", "Ok");
        }
    }

    private async void ImgBtnInstagram_Clicked(object sender, EventArgs e)
    {
        var instagramUrl = "https://www.instagram.com/nunosalgueiro23/";

        if (Uri.TryCreate(instagramUrl, UriKind.Absolute, out var uri))
        {
            await Launcher.OpenAsync(uri);
        }
        else
        {
            await DisplayAlert("Error", "Unable to open Instagram page", "Ok");
        }
    }

    private async void ImgBtnGitHub_Clicked(object sender, EventArgs e)
    {
        var githubUrl = "https://github.com/nunosalg";

        if (Uri.TryCreate(githubUrl, UriKind.Absolute, out var uri))
        {
            await Launcher.OpenAsync(uri);
        }
        else
        {
            await DisplayAlert("Error", "Unable to open GitHub page", "Ok");
        }
    }
}
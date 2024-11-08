namespace FlyWithSalgueiroMobile.Models
{
    public class ProfileImage
    {
        public string? AvatarUrl { get; set; }

        public string? ImagePath => AppConfig.BaseUrl + AvatarUrl;
    }
}

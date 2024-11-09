﻿using CommunityToolkit.Maui;
using FlyWithSalgueiroMobile.Services;
using FlyWithSalgueiroMobile.Validations;
using Microsoft.Extensions.Logging;

namespace FlyWithSalgueiroMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IValidator, Validator>();

            return builder.Build();
        }
    }
}

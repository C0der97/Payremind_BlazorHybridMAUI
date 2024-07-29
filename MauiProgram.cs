using Maui.NullableDateTimePicker;
using Microsoft.Extensions.Logging;
using PayRemind.Shared;
using Plugin.LocalNotification;
using Radzen;
using CommunityToolkit.Maui;

namespace PayRemind
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notifications.db");

            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureNullableDateTimePicker().
                UseLocalNotification().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddRadzenComponents();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new SQLiteDatabaseService(dbPath));


            builder.Services.AddSingleton<NotificationServiceMaui>();
            builder.Services.AddSingleton<NotificationServiceBd>();
            builder.Services.AddSingleton<SharedStateService>();
            return builder.Build();
        }
    }
}
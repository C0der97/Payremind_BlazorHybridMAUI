using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using PayRemind.Contracts;


#if ANDROID
using PayRemind.Platforms.Android;
#endif
using PayRemind.Shared;

namespace PayRemind
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notifications.db");

            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

#if ANDROID
            builder.Services.AddSingleton<IViewConverterService, ViewConverterService>();

            DependencyService.Register<IViewConverterService, ViewConverterService>();
            DependencyService.Register<IAlarmService, AlarmManagerService>();

           // builder.Services.AddTransient<IAlarmService, AlarmManagerService>();

#endif

            builder.Services.AddSingleton(new SQLiteDatabaseService(dbPath));

            builder.Services.AddSingleton<NotificationServiceMaui>();
            builder.Services.AddSingleton<NotificationServiceBd>();
            builder.Services.AddSingleton<SharedStateService>();
            return builder.Build();
        }
    }
}
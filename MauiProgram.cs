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
            }).UseMauiCommunityToolkit().UseSentry(options => {
                // The DSN is the only required setting.
                options.Dsn = "https://42213245402bf788a901d07f056950d4@o4507708174958592.ingest.us.sentry.io/4507708177448960";

                // Use debug mode if you want to see what the SDK is doing.
                // Debug messages are written to stdout with Console.Writeline,
                // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                // This option is not recommended when deploying your application.
                options.Debug = true;

                // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                options.TracesSampleRate = 1.0;

                // Other Sentry options can be set here.
            });

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
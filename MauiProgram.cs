using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using PayRemind.Contracts;



#if ANDROID
using PayRemind.Platforms.Android;
using PayRemind.Platforms.Android.Services;

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
            builder.UseMauiApp<App>()
                .UseSentry(options => {
                    options.Dsn = "https://d1a711d4880a4dfab51453a6e0c83728@o4507708174958592.ingest.us.sentry.io/4507832608882688";
                    options.Debug = true;
                    options.TracesSampleRate = 1.0;
                })
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            //builder.Services.AddNotifications();

            //builder.Services.AddJob(typeof(MyBackgroundJob), 
            //    requiredNetwork: Shiny.Jobs.InternetAccess.None,
            //    runInForeground: true, identifier: "notifications"
            //    );




#if ANDROID


            builder.Services.AddSingleton<IViewConverterService, ViewConverterService>();

            DependencyService.Register<IViewConverterService, ViewConverterService>();
            DependencyService.Register<IAlarmService, AlarmManagerService>();
            DependencyService.Register<IForegroundService, ForegroundServiceImplementation>();

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
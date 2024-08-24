using Android.App;
using Android.Content;
using Android.OS;
using CommunityToolkit.Maui.Alerts;
using PayRemind.Contracts;

//using Shiny.Notifications;
using Application = Microsoft.Maui.Controls.Application;
//using Notification = Shiny.Notifications.Notification;

namespace PayRemind
{
    public partial class App : Application
    {

        public static bool AppActive = false;

        public static bool TutorialDone = false;


        public static readonly AppTheme CurrentTheme = Application.Current == null ? AppTheme.Dark : Application.Current.RequestedTheme;


        public IForegroundService ServiceForeground { get; set; }


        //public INotificationManager NotificationManager { get; set; }

        public App(/*INotificationManager notificationManager, */ IForegroundService serviceForeground)
        {

            ServiceForeground = serviceForeground;
            InitializeComponent();

            //NotificationManager = notificationManager;

            MainPage = new MainPage(serviceForeground);
        }

        protected override async void OnStart()
        {
            base.OnStart();



            Dispatcher.Dispatch(async () =>
            {
                await Permissions.RequestAsync<Permissions.Reminders>();
                await Permissions.RequestAsync<Permissions.Battery>();
                await Permissions.RequestAsync<Permissions.PostNotifications>();
                await Permissions.RequestAsync<Permissions.Camera>();
                await Permissions.RequestAsync<Permissions.Flashlight>();
                await Permissions.RequestAsync<Permissions.LaunchApp>();

#if ANDROID
                Android.App.Activity? context = Platform.CurrentActivity;
                string? packageName = context?.PackageName ?? "";

                if (context?.GetSystemService(Context.PowerService) is PowerManager powerManager && !powerManager.IsIgnoringBatteryOptimizations(packageName))
                {
                    var intent = new Intent();
                    intent?.SetAction(Android.Provider.Settings.ActionRequestIgnoreBatteryOptimizations);
                    intent?.SetData(Android.Net.Uri.Parse($"package:{packageName}"));
                    context?.StartActivity(intent);
                }


                if (OperatingSystem.IsAndroidVersionAtLeast(31)) // Android 12 y superior
                {
                    if (context?.GetSystemService(Context.AlarmService) is AlarmManager alarmManager && !alarmManager.CanScheduleExactAlarms())
                    {
                        // Necesitamos solicitar permiso al usuario
                        var intent = new Intent(Android.Provider.Settings.ActionRequestScheduleExactAlarm);
                        intent.AddFlags(ActivityFlags.NewTask);
                        context?.StartActivity(intent);
                        await MainPage?.DisplayAlert("Permiso requerido", "Por favor, otorga permiso para programar alarmas exactas en la siguiente pantalla.", "OK");
                        return;
                    }
                }

                //IForegroundService _foregroundService = DependencyService.Get<IForegroundService>();

                //_foregroundService.StartForegroundService();


#endif

            });













            if (CurrentTheme == AppTheme.Light)
            {
                this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#ffffff"), Color.FromArgb("#ffffff"));
            }
            else
            {
                this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#32323d"), Color.FromArgb("#32323d"));
            }


            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged += (s, a) =>
                {
                    if (a.RequestedTheme == AppTheme.Light)
                    {
                        this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#ffffff"), Color.FromArgb("#ffffff"));
                    }
                    else
                    {
                        this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#32323d"), Color.FromArgb("#32323d"));
                    }
                };
            }

            //AccessState response =  await NotificationManager.RequestAccess(AccessRequestFlags.Notification);

            //if (response != AccessState.Available)
            //{
            //   await MainPage.DisplaySnackbar("No permisos", null, "OK");
            //}
            //else
            //{
            //    await MainPage.DisplaySnackbar("Si permisos", null, "OK");
            //}
        }


        protected override void OnSleep()
        {
            ServiceForeground.StartForegroundService();
            base.OnSleep();
        }


        protected override void OnResume()
        {
            ServiceForeground.StopForegroundService();
            base.OnResume();
        }

        //        protected override Window CreateWindow(IActivationState activationState)
        //        {

        //            var mainPage = new MainPage();
        //            var tabbedPage = (TabbedPage)mainPage;

        //            var cosa = activationState.State;

        //            //// Aquí puedes recuperar el índice de la pestaña de los extras del Intent
        //            //var intent = activationState.GetValueOrDefault("Intent") as Intent;
        //            //if (intent != null)
        //            //{
        //            //    int tabIndex = intent.GetIntExtra("TabIndex", -1);
        //            //    if (tabIndex >= 0 && tabIndex < tabbedPage.Children.Count)
        //            //    {
        //            //        tabbedPage.CurrentPage = tabbedPage.Children[tabIndex];
        //            //    }
        //            //}
        //            // Puedes personalizar la creación de la ventana aquí si es necesario


        //#if ANDROID

        //#endif

        //            MessagingCenter.Subscribe<object, int>(this, "NavigateToTab", (sender, tabIndex) =>
        //            {
        //                if (MainPage is TabbedPage tabbedPage)
        //                {
        //                    if (tabIndex >= 0 && tabIndex < tabbedPage.Children.Count)
        //                    {
        //                        tabbedPage.CurrentPage = tabbedPage.Children[tabIndex];
        //                    }
        //                }
        //            });


        //            return new Window(mainPage);
        //        }

        //protected override Window CreateWindow(IActivationState activationState)
        //{
        //    MainPage mainPage = new MainPage();
        //    return new Window(mainPage);
        //}
    }

   
}

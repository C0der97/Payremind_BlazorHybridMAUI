using Android.App;
using Android.Content;
using Android.OS;
using CommunityToolkit.Maui.Alerts;
using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;
using Application = Microsoft.Maui.Controls.Application;
using Notification = Shiny.Notifications.Notification;

namespace PayRemind
{
    public partial class App : Application
    {

        public static bool AppActive = false;

        public INotificationManager NotificationManager { get; set; }

        public App(INotificationManager notificationManager, IJobManager jobManager)
        {
            InitializeComponent();

            NotificationManager = notificationManager;


            MainPage = new MainPage(notificationManager, jobManager);
        }

        protected override async void OnStart()
        {
            base.OnStart();



            await Permissions.RequestAsync<Permissions.Reminders>();
            await Permissions.RequestAsync<Permissions.Battery>();
            await Permissions.RequestAsync<Permissions.PostNotifications>();
            await Permissions.RequestAsync<Permissions.Camera>();
            await Permissions.RequestAsync<Permissions.Flashlight>();


            //AccessState response =  await NotificationManager.RequestAccess(AccessRequestFlags.Notification);

            //if (response != AccessState.Available)
            //{
            //   await MainPage.DisplaySnackbar("No permisos", null, "OK");
            //}
            //else
            //{
            //    await MainPage.DisplaySnackbar("Si permisos", null, "OK");
            //}

#if ANDROID
            Android.App.Activity? context = Platform.CurrentActivity;
                        string? packageName = context?.PackageName ?? "";
                        PowerManager? powerManager = context?.GetSystemService(Context.PowerService) as PowerManager;

                        if (powerManager != null && !powerManager.IsIgnoringBatteryOptimizations(packageName))
                        {
                            var intent = new Intent();
                            intent?.SetAction(Android.Provider.Settings.ActionRequestIgnoreBatteryOptimizations);
                            intent?.SetData(Android.Net.Uri.Parse($"package:{packageName}"));
                            context?.StartActivity(intent);
                        }
            #endif
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

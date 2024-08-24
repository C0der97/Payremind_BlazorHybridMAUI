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

        //public INotificationManager NotificationManager { get; set; }

        public App(/*INotificationManager notificationManager, */)
        {
            InitializeComponent();

            //NotificationManager = notificationManager;

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            IForegroundService _foregroundService = DependencyService.Get<IForegroundService>();

            _foregroundService.StartForegroundService();





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

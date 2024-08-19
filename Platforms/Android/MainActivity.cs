using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using PayRemind.Contracts;
using PayRemind.Platforms.Android.Services;

namespace PayRemind
{
    [Activity(Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true,
        Exported = true,
        LaunchMode = LaunchMode.SingleTask,
        ConfigurationChanges = ConfigChanges.ScreenSize | 
        ConfigChanges.Orientation | ConfigChanges.UiMode | 
        ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | 
        ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity? ActivityCurrent { get; set; }
        private static int REQUEST_ID = 1007;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CreateNotificationFromIntent(Intent);

            ActivityCurrent = this;

            App.AppActive = true;


         
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent?
                                             data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Log.Debug("Cosa", "Obteniendo respuesta ");


  

        }

        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent != null && intent?.Extras != null)
            {
                string title = intent?.GetStringExtra(NotificationManagerService.TitleKey);
                string message = intent?.GetStringExtra(NotificationManagerService.MessageKey);

                var service = IPlatformApplication.Current.Services.GetService<INotificationManagerService>();
                service?.ReceiveNotification(title, message);
            }
        }

        protected override void OnNewIntent(Intent? intent)
        {
            CreateNotificationFromIntent(intent);
        }

        private void SetDefaultCallerIdApp()
        {

        }

    }
}

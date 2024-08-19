using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace PayRemind.Platforms.Android
{

    [Service(ForegroundServiceType = ForegroundService.TypeSpecialUse)]
    public class NotificationForegroundServiceOwn : Service
    {
        private const int ServiceId = 1000;
        private const string ChannelId = "ForegroundServiceChannel";

        public override IBinder OnBind(Intent intent) => null;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            CreateNotificationChannel();
            var notification = new Notification.Builder(this, ChannelId)
                .SetContentTitle("Recordatorios")
                .SetContentText("Manteniendo App Viva")
                .Build();

            StartForeground(ServiceId, notification);

            // Your long-running task here

            return StartCommandResult.Sticky;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(ChannelId, "Foreground Service Channel", NotificationImportance.Default);
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }
    }
}

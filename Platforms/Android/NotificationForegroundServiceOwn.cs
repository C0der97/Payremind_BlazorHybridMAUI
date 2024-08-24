using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using PayRemind.Contracts;

namespace PayRemind.Platforms.Android
{

    [Service(ForegroundServiceType = ForegroundService.TypeSpecialUse, 
        Exported = true, 
        Enabled = true)]
    public class NotificationForegroundServiceOwn : Service, IForegroundService
    {
        private const int ServiceId = 1000;
        private const string ChannelId = "ForegroundServiceChannel";

        public override IBinder OnBind(Intent intent) => null;


        public override void OnCreate()
        {
            base.OnCreate();

            CreateNotificationChannel();
            var notification = new Notification.Builder(this, ChannelId)
                .SetContentTitle("Recordatorios")
                .SetContentText("Manteniendo App Viva")
                .Build();

            StartForeground(ServiceId, notification);
        }


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

        public void StartForegroundService()
        {

            Context context = Platform.AppContext;

            var intent = new Intent(context, typeof(NotificationForegroundServiceOwn));
            context.StartForegroundService(intent);
        }

        public void StopForegroundService()
        {
            StopForeground(true);
            StopSelf();
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(ChannelId, "Foreground Service Channel", NotificationImportance.Default);
                NotificationManager? notificationManager = GetSystemService(NotificationService) as NotificationManager;
                notificationManager?.CreateNotificationChannel(channel);
            }
        }


        public override void OnDestroy()
        {
            base.OnDestroy();
            // Aquí puedes reactivar el servicio si se detiene
            Intent broadcastIntent = new Intent(this, typeof(RestartServiceReceiver));
            SendBroadcast(broadcastIntent);
        }


    }
}

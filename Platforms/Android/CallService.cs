using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace PayRemind.Platforms.Android
{
    [Service(ForegroundServiceType = global::Android.Content.PM.ForegroundService.TypeSpecialUse,
        Enabled = true,
        Exported = true)]
    public class CallService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            Intent notificationIntent = new(this, typeof(MainActivity));
            PendingIntent? pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.Immutable);

            //var notification = new Notification.Builder(this, "call_service_channel")
            //    .SetContentTitle("Call Detection Service")
            //    .SetContentText("Detecting incoming calls")
            //    .SetSmallIcon(1)
            //    .SetContentIntent(pendingIntent)
            //    .SetChannelId(ApplicationConstants.ChannelId)
            //    .Build();

            SentrySdk.CaptureMessage("Creando servicio");


            string NOTIFICATION_CHANNEL_ID = "com.companyname.payremind";
            string channelName = "Call service";
            NotificationChannel chan = new(id: NOTIFICATION_CHANNEL_ID,
                                           name: channelName,
                                           importance: NotificationImportance.High);

            chan.LockscreenVisibility = NotificationVisibility.Private;

            NotificationManager manager = (NotificationManager)GetSystemService(Context.NotificationService);
            
            manager.CreateNotificationChannel(chan);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
            Notification notification = notificationBuilder.SetOngoing(true)
                    .SetContentTitle("App is running in background")
                    .SetPriority(1)
                    .SetCategory(Notification.CategoryService)
                    .Build();

            if (OperatingSystem.IsAndroidVersionAtLeast(34))
            {
                StartForeground(1, notification , ForegroundService.TypeSpecialUse);
            }
            else
            {
                StartForeground(1, notification);
            }


            return StartCommandResult.Sticky;
        }
    }
}

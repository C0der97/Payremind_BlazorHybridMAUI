using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace PayRemind.Platforms.Android
{
    [Service(ForegroundServiceType = global::Android.Content.PM.ForegroundService.TypePhoneCall,
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
            //string channelName = "Call service";
            //NotificationChannel chan = new(id: NOTIFICATION_CHANNEL_ID,
            //                               name: channelName,
            //                               importance: NotificationImportance.High);

            //chan.LockscreenVisibility = NotificationVisibility.Private;

            NotificationManager manager = (NotificationManager)GetSystemService(Context.NotificationService);


            var intentAnswer = new Intent(this, typeof(PhoneCallReceiver));

            intentAnswer.SetAction("ANSWER");

            var pendingIntentAnswer = PendingIntent.GetBroadcast(this, 0, intentAnswer,
                PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

            var intentHangup = new Intent(this, typeof(PhoneCallReceiver));
            intentHangup.SetAction("HANGUP");
            var pendingIntentHangup = PendingIntent.GetBroadcast(this, 1, intentHangup,
PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
                );


            //manager.CreateNotificationChannel(chan);


            var channelId = "phone_call_channel";
            var channelName = "Phone Call Notifications";
            var channel = new NotificationChannel(channelId, channelName, NotificationImportance.High)
            {
                LockscreenVisibility = NotificationVisibility.Private
            };
            manager.CreateNotificationChannel(channel);



            var notificationBuilder = new NotificationCompat.Builder(this, channelId)
           .SetContentTitle("Llamada entrante")
           .SetContentText("Tiene una llamada entrante my soc")
           .AddAction(CommunityToolkit.Maui.Resource.Drawable.ic_call_answer, "Answer", pendingIntentAnswer)
           .AddAction(CommunityToolkit.Maui.Resource.Drawable.ic_call_decline, "Hang Up", pendingIntentHangup)
           .SetPriority(NotificationCompat.PriorityHigh)
           .SetOngoing(true);


            //NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
            //Notification notification = notificationBuilder.SetOngoing(true)
            //        .SetContentTitle("App is running in background")
            //        .SetPriority(1)
            //        .SetCategory(Notification.CategoryService)
            //        .Build();

            var notification = notificationBuilder.Build();




           StartForeground(1, notification , ForegroundService.TypePhoneCall);
            


            return StartCommandResult.Sticky;
        }
    }
}

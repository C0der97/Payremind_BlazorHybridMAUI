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

            SentrySdk.CaptureMessage("Creando servicio");


            Java.Lang.Object? serviceData = GetSystemService(Context.NotificationService);

            if (serviceData != null)
            {
                NotificationManager manager = (NotificationManager)serviceData;


                Intent intentAnswer = new(this, typeof(PhoneCallReceiver));

                intentAnswer.SetAction("ANSWER");

                PendingIntent? pendingIntentAnswer = PendingIntent.GetBroadcast(this, 0, intentAnswer,
                    PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

                Intent intentHangup = new(this, typeof(PhoneCallReceiver));

                intentHangup.SetAction("HANGUP");

                PendingIntent? pendingIntentHangup = PendingIntent.GetBroadcast(this, 1, intentHangup,
                                                    PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
                                                    );

                string channelId = "phone_call_channel";
                
                string channelName = "Phone Call Notifications";

                NotificationChannel channel = new(channelId,channelName,NotificationImportance.High)
                {
                    LockscreenVisibility = NotificationVisibility.Private
                };

                manager.CreateNotificationChannel(channel);



                NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, channelId)
               .SetContentTitle("Llamada entrante")
               .SetContentText("Tiene una llamada entrante my soc")
               .SetSmallIcon(CommunityToolkit.Maui.Resource.Drawable.ic_call_answer_low)
               .AddAction(CommunityToolkit.Maui.Resource.Drawable.ic_call_answer, "Answer", pendingIntentAnswer)
               .AddAction(CommunityToolkit.Maui.Resource.Drawable.ic_call_decline, "Hang Up", pendingIntentHangup)
               .SetPriority(NotificationCompat.PriorityHigh)
               .SetOngoing(true);

                Notification notification = notificationBuilder.Build();


                //manager.Notify(1022, notification);

                StartForeground(
                    1,
                    notification,
                    ForegroundService.TypePhoneCall);

            }


            return StartCommandResult.Sticky;
        }
    }
}

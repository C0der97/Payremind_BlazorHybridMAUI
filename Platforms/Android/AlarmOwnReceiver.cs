using Android.App;
using Android.Content;
using Android.Widget;
using AndroidX.Core.App;

namespace PayRemind.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class AlarmOwnReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            var channelId = "alarm_channel";
            var notificationId = 100;

            var notificationBuilder = new NotificationCompat.Builder(context, channelId)
                .SetSmallIcon(Resource.Drawable.mtrl_ic_indeterminate)
                .SetContentTitle("Alarma")
                .SetContentText("¡Es hora de tu alarma!")
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetAutoCancel(true);

            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            if (OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                var channel = new NotificationChannel(channelId, "Alarmas", NotificationImportance.High);
                notificationManager.CreateNotificationChannel(channel);
            }

            notificationManager.Notify(notificationId, notificationBuilder.Build());
        }


    }
}

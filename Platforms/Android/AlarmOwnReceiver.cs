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
            Random rnd = new Random();

            var notificationId = rnd.Next();


            string valueString = intent?.GetStringExtra("name_reminder") ?? "";

            var notificationBuilder = new NotificationCompat.Builder(context, channelId)
                .SetSmallIcon(Resource.Drawable.mtrl_ic_indeterminate)
                .SetContentTitle("Recordatorio de Pago ")
                .SetContentText(valueString)
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

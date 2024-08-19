using Android.App;
using Android.Content;
using Android.Widget;
using AndroidX.Core.App;

namespace PayRemind.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class AlarmOwnReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context? context, Intent? intent)
        {

            if (intent == null || context == null)
            {
                return;
            }

            var channelId = "alarm_channel";
            Random rnd = new();

            var notificationId = rnd.Next();


            string valueString = intent?.GetStringExtra("name_reminder") ?? "";

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, channelId)
                .SetSmallIcon(Resource.Drawable.mtrl_ic_indeterminate)
                .SetContentTitle("Recordatorio de Pago ")
                .SetContentText(valueString)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetAutoCancel(true);

            if (context != null)
            {
                NotificationManager? notificationManager = context?.GetSystemService(Context.NotificationService) as NotificationManager;

                if (OperatingSystem.IsAndroidVersionAtLeast(26))
                {
                    NotificationChannel channel = new(channelId, "Alarmas", NotificationImportance.High);
                    notificationManager?.CreateNotificationChannel(channel);
                }

                notificationManager?.Notify(notificationId, notificationBuilder.Build());
            }

      
        }


    }
}

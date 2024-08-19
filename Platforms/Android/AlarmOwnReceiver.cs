using Android.App;
using Android.Content;
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
            int notificationId = intent.GetIntExtra("alarm_id", 0);  // Usa el ID único
            string valueString = intent.GetStringExtra("name_reminder") ?? "";
            string title = intent.GetStringExtra("title_reminder") ?? "";

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, channelId)
                .SetSmallIcon(Resource.Drawable.mtrl_ic_indeterminate)
                .SetContentTitle("Recordatorio de Pago " + title)
                .SetContentText(valueString)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetAutoCancel(true)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);  // Añade sonido y vibración

            NotificationManager? notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            if (OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                NotificationChannel channel = new NotificationChannel(channelId, "Alarmas", NotificationImportance.High);
                channel.EnableVibration(true);
                channel.EnableLights(true);
                notificationManager?.CreateNotificationChannel(channel);
            }

            notificationManager?.Notify(notificationId, notificationBuilder.Build());
        }
    }
}
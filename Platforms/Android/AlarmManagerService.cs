using Android.App;
using Android.Content;
using PayRemind.Contracts;

namespace PayRemind.Platforms.Android
{
    public class AlarmManagerService : IAlarmService
    {
        public void SetAlarm(DateTime alarmTime, string name_reminder)
        {
            Context context = Platform.AppContext;
            long triggerAtMillis = new DateTimeOffset(alarmTime.ToUniversalTime()).ToUnixTimeMilliseconds();

            AlarmManager? alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
            Intent intent = new(context, typeof(AlarmOwnReceiver));

            intent.PutExtra("name_reminder", name_reminder);


            PendingIntent? pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

            if (pendingIntent != null)
            {
                if (OperatingSystem.IsAndroidVersionAtLeast(31)) // Android 12 y superior
                {
                    alarmManager?.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, triggerAtMillis, pendingIntent);
                }
                else
                {
                    alarmManager?.SetExact(AlarmType.RtcWakeup, triggerAtMillis, pendingIntent);
                }
            }

      
        }
    }
}

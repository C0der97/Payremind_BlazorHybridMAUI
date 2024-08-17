using Android.App;
using Android.Content;
using PayRemind.Contracts;

namespace PayRemind.Platforms.Android
{
    public class AlarmManagerService : IAlarmService
    {
        public void SetAlarm(DateTime alarmTime)
        {
            var context = Platform.AppContext;
            var triggerAtMillis = new DateTimeOffset(alarmTime).ToUnixTimeMilliseconds();

            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            var intent = new Intent(context, typeof(AlarmOwnReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

            if (OperatingSystem.IsAndroidVersionAtLeast(31)) // Android 12 y superior
            {
                alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, triggerAtMillis, pendingIntent);
            }
            else
            {
                alarmManager.SetExact(AlarmType.RtcWakeup, triggerAtMillis, pendingIntent);
            }
        }
    }
}

using Android.App;
using Android.Content;
using PayRemind.Contracts;
using static Android.Icu.Text.CaseMap;

namespace PayRemind.Platforms.Android
{
    public class AlarmManagerService : IAlarmService
    {
        public void SetAlarm(DateTime alarmTime, string name_reminder, string tittle)
        {
            Context context = Platform.AppContext;
            long triggerAtMillis = new DateTimeOffset(alarmTime.ToUniversalTime()).ToUnixTimeMilliseconds();
            AlarmManager? alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;

            // Genera un ID único para cada alarma
            int uniqueId = unchecked((int)DateTime.Now.Ticks);

            Intent intent = new(context, typeof(AlarmOwnReceiver));
            intent.PutExtra("name_reminder", name_reminder);
            intent.PutExtra("title_reminder", tittle);
            intent.PutExtra("alarm_id", uniqueId);  // Añade el ID único a los extras

            PendingIntent? pendingIntent = PendingIntent.GetBroadcast(context, uniqueId, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

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

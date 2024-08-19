using Android.App;
using Android.Content;

namespace PayRemind.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context? context, Intent? intent)
        {
            if (intent.Action == Intent.ActionBootCompleted)
            {
                // Aquí deberías implementar la lógica para recuperar todas las alarmas guardadas
                // y reprogramarlas usando el AlarmManagerService

                // Ejemplo:
                // var alarmService = new AlarmManagerService();
                // foreach (var alarm in SavedAlarms)
                // {
                //     alarmService.SetAlarm(alarm.DateTime, alarm.Name, alarm.Title);
                // }
            }
        }
    }
}
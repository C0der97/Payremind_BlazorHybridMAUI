using Android.Content;

namespace PayRemind.Platforms.Android
{

    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class RestartServiceReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            context.StartService(new Intent(context, typeof(NotificationForegroundServiceOwn)));
        }
    }
}

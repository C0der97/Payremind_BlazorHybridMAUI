using Android.App;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace PayRemind.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter([Intent.ActionDial], Categories = [Intent.CategoryDefault])]
    [IntentFilter([Intent.ActionView], Categories = [Intent.CategoryDefault, Intent.CategoryBrowsable])]
    public class DialReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Log.Debug("Cosa", "Se mete al Dial Reveiver");


            // Aquí puedes manejar el Intent ACTION_DIAL
            string data = intent.Data.ToString();
            Toast.MakeText(context, $"Dial Intent received: {data}", ToastLength.Long).Show();

            // Opcionalmente, iniciar una actividad para mostrar un teclado de marcado
            var dialIntent = new Intent(Intent.ActionCall, intent.Data);
            dialIntent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(dialIntent);
        }
    }
}

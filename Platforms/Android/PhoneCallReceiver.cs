using Android.App;
using Android.Content;
using Android.Telephony;
using Android.Util;

namespace PayRemind.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { TelephonyManager.ActionPhoneStateChanged })]
    public class PhoneCallReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != TelephonyManager.ActionPhoneStateChanged)
            {
                Log.Debug("Cosa", "Received intent with non-phone state action");
                return;

            }

            //string state = intent.GetStringExtra(TelephonyManager.ExtraState);
            //if (state == TelephonyManager.ExtraStateRinging)
            //{
            //    string incomingNumber = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);
            //    MainThread.BeginInvokeOnMainThread(() =>
            //    {
            //        MessagingCenter.Send<object, string>(this, "IncomingCall", incomingNumber);
            //    });

            //}


            //string state = intent.GetStringExtra(TelephonyManager.ExtraState);
            //if (state == TelephonyManager.ExtraStateRinging)
            //{
            //    string incomingNumber = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);

            //    // Abre la aplicación y navega a la página de llamadas
            //    var startIntent = new Intent(context, typeof(MainActivity));
            //    startIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            //    startIntent.PutExtra("OpenCallPage", true);
            //    startIntent.PutExtra("IncomingNumber", incomingNumber);
            //    context.StartActivity(startIntent);
            //}


            string state = intent.GetStringExtra(TelephonyManager.ExtraState);
            if (state == TelephonyManager.ExtraStateRinging)
            {

                var serviceIntent = new Intent(context, typeof(CallService));

                context.StartForegroundService(serviceIntent);

                string incomingNumber = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);


                SentrySdk.CaptureMessage("Recibiendo llamada número "+incomingNumber);


                //MainThread.BeginInvokeOnMainThread(() =>
                //{
                //    MessagingCenter.Send(this, "NavigateToTab", 2);
                //});



                //// Abre la aplicación y navega a la página de llamadas
                ///




                //Intent launchIntent = context.PackageManager.GetLaunchIntentForPackage("com.companyname.payremind");

                var launchIntent = new Intent(context, typeof(MainActivity));

                if (launchIntent != null)
                {

                    SentrySdk.CaptureMessage("Invocando launchIntent");


                    launchIntent.AddFlags(ActivityFlags.NewTask);
                    launchIntent.PutExtra("OpenCallPage", true);
                    launchIntent.PutExtra("IncomingNumber", incomingNumber);
                    context.StartActivity(launchIntent);
                }

                if (App.AppActive)
                {


                    //var startIntent = new Intent(context, typeof(MainActivity));
                    ////startIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    //startIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    //startIntent.PutExtra("OpenCallPage", true);
                    //startIntent.PutExtra("IncomingNumber", incomingNumber);
                    //context.StartActivity(startIntent);
                }
                else
                {
                    //WeakReferenceMessenger.Default.Send(new TabIndexMessage(2));

                }
            }

        }
    }
    }

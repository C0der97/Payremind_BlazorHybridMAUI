using Android;
using Android.App;
using Android.App.Roles;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using CommunityToolkit.Mvvm.Messaging;
using PayRemind.Messages;
using PayRemind.Pages;
using PayRemind.Platforms.Android;

namespace PayRemind
{
    [Activity(Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | 
        ConfigChanges.Orientation | ConfigChanges.UiMode | 
        ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | 
        ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity ActivityCurrent { get; set; }
        private static int REQUEST_ID = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActivityCurrent = this;

            App.AppActive = true;


            if (Intent.HasExtra("incoming_number"))
            {
                var incomingNumber = Intent.GetStringExtra("incoming_number");
                //Microsoft.Maui.Controls.Application.Current.MainPage = new CallPage(incomingNumber);

                Microsoft.Maui.Controls.Application.Current.MainPage = new NavigationPage(new CallPage(incomingNumber));

            }


            if (savedInstanceState == null)
            {
                RoleManager roleManager = (RoleManager)GetSystemService(RoleService);
                // Verificar si se debe abrir una página específica

                Intent intent = roleManager.CreateRequestRoleIntent(RoleManager.RoleCallScreening);
                StartActivityForResult(intent, REQUEST_ID);


                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ManageOwnCalls) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ManageOwnCalls }, 1001);
                }


                //var serviceIntent = new Intent(this, typeof(CallService));
                //StartForegroundService(serviceIntent);
            }


        

            if (Intent.GetBooleanExtra("OpenCallPage", false))
            {
                string incomingNumber = Intent.GetStringExtra("IncomingNumber");
                // Navega a la página de llamadas (esto dependerá de cómo esté estructurada tu app)
                // Por ejemplo, podrías usar un evento global o un servicio de mensajería
                // para notificar a tu Blazor WebView que debe navegar a la página de llamadas
                // y pasar el número entrante.

                //MessagingCenter.Send(this, "NavigateToTab", 2);

                SentrySdk.CaptureMessage("Llamada a OpenCallPage222");

                WeakReferenceMessenger.Default.Send(new TabIndexMessage(2, incomingNumber ?? ""));


                //OpenCallsTab();
            }
        }


        private void OpenCallsTab()
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

            var callsTabIndex = 2; // Índice de la pestaña "Calls"
            var bundle = new Bundle();
            bundle.PutInt("TabIndex", callsTabIndex);
            intent.PutExtras(bundle);

            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent
                                             data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUEST_ID)
            {
                if (resultCode == Result.Ok)
                {


                }
                else
                {
                    // The resultCode is always Canceled
                }
            }

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (intent != null)
            {

                if (intent.GetStringExtra("incoming_number") != string.Empty)
                {
                    var incomingNumber = intent.GetStringExtra("incoming_number");
                    Microsoft.Maui.Controls.Application.Current.MainPage = new CallPage(incomingNumber);
                }


                if (intent.GetBooleanExtra("OpenCallPage", false))
                {
                    string incomingNumber = intent.GetStringExtra("IncomingNumber");

                    //WeakReferenceMessenger.Default.Send(new TabIndexMessage(2));

                    //var callsTabIndex = 2; // Índice de la pestaña "Calls"
                    //var bundle = new Bundle();
                    //bundle.PutInt("TabIndex", callsTabIndex);
                    //intent.PutExtras(bundle);

                    //StartActivity(intent);


                    SentrySdk.CaptureMessage("Llamada de "+ incomingNumber);

                    //WeakReferenceMessenger.Default.Send(new TabIndexMessage(2, incomingNumber ?? ""));

                    SentrySdk.CaptureMessage("Llamada a OnNewIntent Nueva");


                    // Realiza la acción necesaria
                }
            }
        }
    }
}

using Android.Content;
using Android.Runtime;
using Android.Telecom;
using Android.Telephony;
using PayRemind.Contracts;
using PayRemind.Platforms.Android;
using CallState = Android.Telephony.CallState;


[assembly: Dependency(typeof(CallHandlingService))]
namespace PayRemind.Platforms.Android
{
    public class CallHandlingService : PhoneStateListener, ICallHandler
    {
        private readonly TelecomManager _telecomManager;

        private readonly Context _context;

        public CallHandlingService()
        {
            _context = MauiApplication.Current.ApplicationContext;
        }

        public void AnswerCall()
        {
            var telecomManager = (TelecomManager)_context.GetSystemService(Context.TelecomService);
            telecomManager.AcceptRingingCall();
        }

        public void RejectCall()
        {
            var telecomManager = (TelecomManager)_context.GetSystemService(Context.TelecomService);
            telecomManager.EndCall();
        }

        //public override void OnCallStateChanged([GeneratedEnum]
        //CallState state, string? phoneNumber)
        //{
        //    base.OnCallStateChanged(state, phoneNumber);

        //    if (state == CallState.Ringing)
        //    {

        //    }
        //}
    }
}

using Android.App;
using Android.Telecom;

namespace PayRemind.Platforms.Android
{
    [Service(Exported = true, Permission = "android.permission.BIND_SCREENING_SERVICE")]
    [IntentFilter(["android.telecom.CallScreeningService"])]
    public class IncomingCallService : CallScreeningService
    {
        public override void OnScreenCall(Call.Details callDetails)
        {
            global::Android.Net.Uri? handle = callDetails.GetHandle();
        }
    }
}

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Telecom;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace PayRemind.Platforms.Android
{
    [Service(Enabled = true, Exported = true,
        Permission = "android.permission.BIND_INCALL_SERVICE"
        )]
    [IntentFilter(["android.telecom.InCallService"])]
    [MetaData(
        "android.telecom.IN_CALL_SERVICE_UI",
        Value = "true"
    )]
    public class CallService : InCallService
    {
        private readonly Call.Callback _callCallback = new CallCallback();

        public override void OnCallAdded(Call call)
        {
            base.OnCallAdded(call);
            call.RegisterCallback(_callCallback);

            // Check the screen state and whether to show the notification
            bool isScreenLocked = ((KeyguardManager)GetSystemService(Context.KeyguardService)).IsKeyguardLocked;
            bool isDeviceInteractive = ((PowerManager)GetSystemService(Context.PowerService)).IsInteractive;

            if (!isDeviceInteractive || isScreenLocked)
            {
                ShowNotification(call);
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                ShowNotification(call);
            }
        }

        public override void OnCallRemoved(Call call)
        {
            base.OnCallRemoved(call);
            call.UnregisterCallback(_callCallback);
            CancelNotification();
        }

        private void ShowNotification(Call call)
        {
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            string channelId = "phone_call_channel";
            string channelName = "Phone Call Notifications";

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(channelId, channelName, NotificationImportance.High)
                {
                    LockscreenVisibility = NotificationVisibility.Private
                };
                notificationManager.CreateNotificationChannel(channel);
            }

            var notificationIntent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.Immutable);

            var intentAnswer = new Intent(this, typeof(PhoneCallReceiver));
            intentAnswer.SetAction("ANSWER");
            var pendingIntentAnswer = PendingIntent.GetBroadcast(this, 0, intentAnswer, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

            var intentHangup = new Intent(this, typeof(PhoneCallReceiver));
            intentHangup.SetAction("HANGUP");
            var pendingIntentHangup = PendingIntent.GetBroadcast(this, 1, intentHangup, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

            var notificationBuilder = new NotificationCompat.Builder(this, channelId)
                .SetContentTitle("Llamada entrante")
                .SetContentText("Tiene una llamada entrante.")
                .SetSmallIcon(Resource.Drawable.ic_call_answer_low) // Ensure you have this drawable
                .AddAction(Resource.Drawable.ic_call_answer, "Answer", pendingIntentAnswer)
                .AddAction(Resource.Drawable.ic_call_decline, "Hang Up", pendingIntentHangup)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetOngoing(true);

            Notification notification = notificationBuilder.Build();

            notificationManager.Notify(10203, notification);

            //StartForeground(1, notification, ForegroundService.TypePhoneCall);
        }

        private void CancelNotification()
        {
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Cancel(1);
        }

        private class CallCallback : Call.Callback
        {
            public override void OnStateChanged(Call call, CallState state)
            {
                base.OnStateChanged(call, state);
                if (state == CallState.Disconnected || state == CallState.Disconnecting)
                {
                    // Handle call disconnected or disconnecting
                }
            }
        }
    }
}

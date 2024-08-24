using Android.Content;
using PayRemind.Contracts;

namespace PayRemind.Platforms.Android.Services
{
    public class ForegroundServiceImplementation : IForegroundService
    {
        public void StartForegroundService()
        {
            Context context = Platform.AppContext;

            var intent = new Intent(context, typeof(NotificationForegroundServiceOwn));
            context.StartForegroundService(intent);
        }

        public void StopForegroundService()
        {
            Context context = Platform.AppContext;
            var intent = new Intent(context, typeof(NotificationForegroundServiceOwn));
            context.StopService(intent);
        }
    }
}

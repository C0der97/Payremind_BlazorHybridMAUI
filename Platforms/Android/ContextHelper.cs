using Android.Content;

namespace PayRemind.Platforms.Android
{
    public static class ContextHelper
    {
        public static Context GetContext()
        {
            return Platform.AppContext;
        }
    }
}

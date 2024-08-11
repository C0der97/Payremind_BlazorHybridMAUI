using Microsoft.Maui.Platform;
using PayRemind.Contracts;

namespace PayRemind.Platforms.Android
{
    public class ViewConverterService : IViewConverterService
    {
        public global::Android.Views.View GetNativeView(Microsoft.Maui.Controls.View view)
        {
            if (view.Handler != null)
            {
                // Obtén la vista nativa usando ToPlatform
                global::Android.Views.View platformView = view.ToPlatform(view.Handler.MauiContext);
                return platformView;
            }

            throw new InvalidOperationException("El Handler de la vista es null.");
        }
    }
}

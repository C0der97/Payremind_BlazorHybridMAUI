using Android.Views;
using Microsoft.Maui.Platform;
using Smartdevelop.IR.Eram.Showcaseviewlib;
using Smartdevelop.IR.Eram.Showcaseviewlib.Config;
using Gravity = Smartdevelop.IR.Eram.Showcaseviewlib.Config.Gravity;

namespace PayRemind.MauiWrapper
{
    public class ShowCaseViewWrapper
    {
        public static void ShowGuideView(Android.Views.View targetView, string title, string content)
        {
            var activity = Platform.CurrentActivity;
            //var nativeView = targetView.ToPlatform(Platform.AppContext);

            if (activity is not null)
            {
                GuideView.Builder? builder = new GuideView.Builder(activity)
                        ?.SetTitle(title)
                        ?.SetContentText(content)
                        ?.SetTargetView(targetView)
                        ?.SetGravity(Gravity.Auto)
                        ?.SetDismissType(DismissType.Anywhere)
                        ?.SetPointerType(PointerType.Circle);

                GuideView? guideView = builder?.Build();
                guideView?.Show();


            }

    
        }
    }
}

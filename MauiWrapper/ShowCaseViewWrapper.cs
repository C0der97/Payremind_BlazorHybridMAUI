using Android.Views;
using Microsoft.Maui.Platform;
using Smartdevelop.IR.Eram.Showcaseviewlib;
using Smartdevelop.IR.Eram.Showcaseviewlib.Config;
using Smartdevelop.IR.Eram.Showcaseviewlib.Listener;
using Gravity = Smartdevelop.IR.Eram.Showcaseviewlib.Config.Gravity;

namespace PayRemind.MauiWrapper
{
    public class ShowCaseViewWrapper
    {
        public static void ShowGuideView(Android.Views.View targetView1, Android.Views.View targetView2, string title1, string content1, string title2, string content2)
        {
            Android.App.Activity? activity = Platform.CurrentActivity;

            if (activity is not null)
            {
                GuideView.Builder? guideViewBuilder = new GuideView.Builder(activity)
                    ?.SetGravity(Gravity.Auto)
                    ?.SetDismissType(DismissType.Anywhere)
                    ?.SetPointerType(PointerType.Circle);

                // Configuración del primer paso
                guideViewBuilder
                    ?.SetTitle(title1)
                    ?.SetContentText(content1)
                    ?.SetTargetView(targetView1)
                    ?.SetGuideListener(new GuideListener(1, targetView2, title2, content2));

                // Construir y mostrar el primer paso
                GuideView? guideView = guideViewBuilder?.Build();
                guideView?.Show();
            }
        }
    }

    public class GuideListener : Java.Lang.Object, IGuideListener
    {
        private readonly int _nextStep;
        private readonly Android.Views.View _nextTargetView;
        private readonly string _nextTitle;
        private readonly string _nextContent;

        public GuideListener(int nextStep, Android.Views.View nextTargetView, string nextTitle, string nextContent)
        {
            _nextStep = nextStep;
            _nextTargetView = nextTargetView;
            _nextTitle = nextTitle;
            _nextContent = nextContent;
        }

        public void OnDismiss(Android.Views.View? view)
        {

            GuideView.Builder? builder = new GuideView.Builder(view.Context)
                                ?.SetTitle(_nextTitle)
                                ?.SetContentText(_nextContent)
                                ?.SetTargetView(_nextTargetView)
                                ?.SetGravity(Gravity.Center)
                                ?.SetDismissType(DismissType.Anywhere)
                                ?.SetPointerType(PointerType.Circle);

            GuideView? guideView = builder?.Build();
                                        guideView?.Show();

            Console.WriteLine("Guía desaparecida");
        }

        public void OnGuideViewClicked(Android.Views.View view)
        {
        }
    }
}

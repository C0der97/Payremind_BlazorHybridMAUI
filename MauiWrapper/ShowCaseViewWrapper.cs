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
        public static void ShowGuideView(Android.Views.View targetView, Android.Views.View targetView2, string title, string content)
        {
            var activity = Platform.CurrentActivity;
            //var nativeView = targetView.ToPlatform(Platform.AppContext);

            int step = 1;

            if (activity is not null)
            {
                GuideView.Builder? builder = new GuideView.Builder(activity)
                        ?.SetTitle(title)
                        ?.SetContentText(content)
                        ?.SetTargetView(targetView)
                        ?.SetGravity(Gravity.Auto)
                        ?.SetDismissType(DismissType.Anywhere)
                        ?.SetPointerType(PointerType.Circle);


                IGuideListener guideListener = new GuideListener(1, builder);


                switch (step)
                {
                    case 1:
                        builder?.SetTargetView(targetView2)
                            ?.SetTitle("Guia dos")
                               ?.SetGuideListener(guideListener);
                        break;
                        // Agrega más casos según tus necesidades
                }

                GuideView? guideView = builder?.Build();
                guideView?.Show();


            }


        }
    }



    public class GuideListener : Java.Lang.Object, IGuideListener
    {
        private readonly int _step;
        private readonly GuideView.Builder _builder;

        public GuideListener(int step, GuideView.Builder builder)
        {
            _step = step;
            _builder = builder;
        }

        public void OnDismiss(Android.Views.View? view)
        {
            Console.WriteLine("desapareciendo");
        }

        public void OnGuideViewClicked(Android.Views.View view)
        {
            switch (_step)
            {
                case 0:
                    _builder?.SetTargetView(view)?.Build()?.Show();
                    break;
                case 1:
                    _builder?.SetTargetView(view)?.Build()?.Show();
                    break;
                    // Agrega más pasos según tus necesidades
            }
        }
    }

}


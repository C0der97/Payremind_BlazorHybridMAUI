using Smartdevelop.IR.Eram.Showcaseviewlib.Config;
using Smartdevelop.IR.Eram.Showcaseviewlib;
using Android.Content;
using PayRemind.Platforms.Android.Wrappers;
using Android.App;
using Microsoft.Maui.Platform;

namespace PayRemind.Wrappers
{

    public class ShowCase
    {
        //private Context _context;

        //public ShowCaseService(Context context)
        //{
        //    _context = context;
        //}

        //public void ShowFeatureHighlight(int viewId, string title, string description)
        //{
        //    // Obtén la vista usando el contexto
        //    var activity = (Activity)_context;
        //    var view = activity.FindViewById(viewId);


        //    new GuideView.Builder(_context)
        //        .SetTitle("Guide Title Text")
        //        .SetContentText("Guide Description Text\n .....Guide Description Text\n .....Guide Description Text .....")
        //        .SetGravity(Gravity.Auto) //optional
        //        .SetDismissType(DismissType.Anywhere) //optional - default DismissType.targetView
        //        .SetTargetView(view)
        //        .SetContentTextSize(12)//optional
        //        .SetTitleTextSize(14)//optional
        //        .Build()
        //        .Show();
        //}

        public static void ShowGuideView(int viewId, string title, string content)
        {
            var activity = Platform.CurrentActivity;
            //var nativeView = targetView.ToPlatform(activity);

            var view = activity.FindViewById(viewId);

            var builder = new GuideView.Builder(activity)
                .SetTitle(title)
                .SetContentText(content)
                .SetTargetView(view)
                .SetGravity(Gravity.Auto)
                .SetDismissType(DismissType.Anywhere);

            var guideView = builder.Build();
            guideView.Show();
        }
    }
}

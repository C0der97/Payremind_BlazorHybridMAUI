namespace PayRemind
{
    public partial class App : Application
    {

        public static bool AppActive = false;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        //        protected override Window CreateWindow(IActivationState activationState)
        //        {

        //            var mainPage = new MainPage();
        //            var tabbedPage = (TabbedPage)mainPage;

        //            var cosa = activationState.State;

        //            //// Aquí puedes recuperar el índice de la pestaña de los extras del Intent
        //            //var intent = activationState.GetValueOrDefault("Intent") as Intent;
        //            //if (intent != null)
        //            //{
        //            //    int tabIndex = intent.GetIntExtra("TabIndex", -1);
        //            //    if (tabIndex >= 0 && tabIndex < tabbedPage.Children.Count)
        //            //    {
        //            //        tabbedPage.CurrentPage = tabbedPage.Children[tabIndex];
        //            //    }
        //            //}
        //            // Puedes personalizar la creación de la ventana aquí si es necesario


        //#if ANDROID

        //#endif

        //            MessagingCenter.Subscribe<object, int>(this, "NavigateToTab", (sender, tabIndex) =>
        //            {
        //                if (MainPage is TabbedPage tabbedPage)
        //                {
        //                    if (tabIndex >= 0 && tabIndex < tabbedPage.Children.Count)
        //                    {
        //                        tabbedPage.CurrentPage = tabbedPage.Children[tabIndex];
        //                    }
        //                }
        //            });


        //            return new Window(mainPage);
        //        }

        //protected override Window CreateWindow(IActivationState activationState)
        //{
        //    MainPage mainPage = new MainPage();
        //    return new Window(mainPage);
        //}
    }
}

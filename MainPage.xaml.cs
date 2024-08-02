using CommunityToolkit.Mvvm.Messaging;
using Microsoft.JSInterop;
using PayRemind.Messages;

namespace PayRemind
{
    public partial class MainPage : TabbedPage
    {
        AppTheme currentTheme = Application.Current.RequestedTheme;

        private DateTime _selectedDateTime;
        public MainPage()
        {
            InitializeComponent();


            if (currentTheme == AppTheme.Light)
            {
                this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#ffffff"), Color.FromArgb("#ffffff"));
            }
            else
            {
                this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#32323d"), Color.FromArgb("#32323d"));
            }

            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                if (a.RequestedTheme == AppTheme.Light)
                {
                    this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#ffffff"), Color.FromArgb("#ffffff"));
                }
                else
                {
                    this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#32323d"), Color.FromArgb("#32323d"));
                }
            };


            WeakReferenceMessenger.Default.Register<TabIndexMessage>(this, (r, message) =>
            {

                SentrySdk.CaptureMessage("Recibiendo evento para cambiar de tab");


                if (message.TabIndex >= 0 && message.TabIndex < this.Children.Count)
                {
                    this.CurrentPage = this.Children[message.TabIndex];
                }
            });

            //MainThread.BeginInvokeOnMainThread(() =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Subscribing to NavigateToTab");
            //    MessagingCenter.Subscribe<object, int>(this, "NavigateToTab", (sender, tabIndex) =>
            //    {
            //        System.Diagnostics.Debug.WriteLine($"Received NavigateToTab message with index {tabIndex}");
            //        if (tabIndex >= 0 && tabIndex < this.Children.Count)
            //        {
            //            this.CurrentPage = this.Children[tabIndex];
            //        }
            //    });
            //});


            //myDatePicker.MinimumDate = new DateTime(2000, 1, 1);
            //myDatePicker.MaximumDate = DateTime.Today;
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            // Aquí puedes manejar el cambio de fecha
            DateTime selectedDate = e.NewDate;
            // Haz algo con la fecha seleccionada
        }

        private void OnTimeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                //_selectedDateTime = _selectedDateTime.Date + timePicker.Time;
                UpdateDateTimeLabel();
            }
        }

        private void UpdateDateTimeLabel()
        {
            //selectedDateTime.Text = $"Selected: {_selectedDateTime:g}";
        }

        [JSInvokable]
        public static Task ShowDatePicker()
        {

            // Aquí puedes mostrar el DatePicker o realizar otra lógica
            // Este ejemplo es simplificado; puede necesitar ajustes según el caso de uso.
            return Task.CompletedTask;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}

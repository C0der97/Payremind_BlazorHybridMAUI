using Android.App;
using Android.Content;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.JSInterop;
using PayRemind.Data;
using PayRemind.Jobs.MyJob;
using PayRemind.Messages;
using Shiny.Jobs;
using Shiny.Notifications;
using Application = Microsoft.Maui.Controls.Application;
using Notification = Shiny.Notifications.Notification;

namespace PayRemind
{
    public partial class MainPage : TabbedPage
    {
        private readonly AppTheme currentTheme = Application.Current == null ? AppTheme.Dark : Application.Current.RequestedTheme;


        private readonly IJobManager _jobManager;

        private bool _isInitialized = false;

        private readonly INotificationManager _notificationManager;


        public MainPage(INotificationManager notificationManager, IJobManager jobManager)
        {
            InitializeComponent();

            _notificationManager = notificationManager;





            if (currentTheme == AppTheme.Light)
            {
                this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#ffffff"), Color.FromArgb("#ffffff"));
            }
            else
            {
                this.SetAppThemeColor(NavigationPage.BarBackgroundProperty, Color.FromArgb("#32323d"), Color.FromArgb("#32323d"));
            }


            if (Application.Current != null)
            {
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
            }

            WeakReferenceMessenger.Default.Register<TabIndexMessage>(this, async (r, message) =>
            {
                InfoStatic.PhoneNumber = message.PhoneNumber ?? "";

                if (message != null)
                {
                    if (!message.DefaultApp)
                    {
                        return;
                    }

                    //if (message.TabIndex >= 0 && message.TabIndex < this.Children.Count)
                    //{
                    //    this.CurrentPage = this.Children[message.TabIndex];
                    //}

                }


                //if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
                //{
                //    await LocalNotificationCenter.Current.RequestNotificationPermission();
                //}

                //await Permissions.RequestAsync<Permissions.Phone>();
                //await Permissions.RequestAsync<Permissions.ContactsRead>();
                //await Permissions.RequestAsync<Permissions.Reminders>();
                //await Permissions.RequestAsync<Permissions.Speech>();
                //await Permissions.RequestAsync<Permissions.Battery>();
                //await Permissions.RequestAsync<Permissions.PostNotifications>();
                //await Permissions.RequestAsync<Permissions.StorageRead>();
                //await Permissions.RequestAsync<Permissions.StorageWrite>();
                //await Permissions.RequestAsync<Permissions.Microphone>();
                //await Permissions.RequestAsync<Permissions.ContactsWrite>();
                //await Permissions.RequestAsync<Permissions.LaunchApp>();

            });


            WeakReferenceMessenger.Default.Register<HideFloatButton>(this, (s, message) =>
            {
                if (message._HideFloatButton)
                {
                    FabButton.IsVisible = false;
                }
                else
                {
                    FabButton.IsVisible = true;
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


            Channel channel = new Channel
            {
                Identifier = "alarms_notif",
                Description = "Channel",
                Importance = ChannelImportance.High,
                Actions = new List<ChannelAction>()
                {
                    new ChannelAction()
                    {
                        Identifier = "alarm_btn",
                        Title = "action_alarm",
                        ActionType = ChannelActionType.OpenApp
                    }
                }
            };


            _notificationManager.AddChannel(channel);

            if (!_isInitialized)
            {
                _isInitialized = true;
                Initialize();
            }


#if ANDROID
            if (OperatingSystem.IsAndroidVersionAtLeast(31)) // Android 12 y superior
            {
                var context = Android.App.Application.Context;
                var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
                if (alarmManager != null && !alarmManager.CanScheduleExactAlarms())
                {
                    // Necesitamos solicitar permiso al usuario
                    var intent = new Intent(Android.Provider.Settings.ActionRequestScheduleExactAlarm);
                    intent.AddFlags(ActivityFlags.NewTask);
                    context.StartActivity(intent);
                    return;
                }
            }



#endif
        }


        private void Initialize()
        {
            //try
            //{
            //    Dispatcher.Dispatch(async () =>
            //    {
            //        //if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            //        //{
            //        //    await LocalNotificationCenter.Current.RequestNotificationPermission();
            //        //}

            //        await Permissions.RequestAsync<Permissions.Reminders>();
            //        await Permissions.RequestAsync<Permissions.Battery>();
            //        await Permissions.RequestAsync<Permissions.PostNotifications>();


            //        if (DeviceInfo.Platform == DevicePlatform.Android)
            //        {
            //            RequestBatteryOptimizationExemption();
            //        }
            //    });
            //}
            //catch (Exception ex)
            //{

            //}
        }


        private void FabButton_Clicked(object sender, EventArgs e)
        {
            //Dispatcher.Dispatch(async () =>
            //{
            //    await OnSendNotificationClicked();
            //});


            WeakReferenceMessenger.Default.Send(new OpenDialog(true));
        }

        private void RequestBatteryOptimizationExemption()
        {

#if ANDROID
            Android.App.Activity? context = Platform.CurrentActivity;
            string? packageName = context?.PackageName ?? "";
            PowerManager? powerManager = (PowerManager)context.GetSystemService(Context.PowerService);

            if (powerManager != null && !powerManager.IsIgnoringBatteryOptimizations(packageName))
            {
                var intent = new Intent();
                intent?.SetAction(Android.Provider.Settings.ActionRequestIgnoreBatteryOptimizations);
                intent?.SetData(Android.Net.Uri.Parse($"package:{packageName}"));
                context?.StartActivity(intent);
            }
#endif


        }


        private async Task OnSendNotificationClicked()
        {
            Shiny.AccessState result = await _notificationManager.RequestAccess(AccessRequestFlags.Notification);




            //JobInfo job = new JobInfo(
            //    "Notifications",
            //    typeof(MyBackgroundJob),
            //    BatteryNotLow: true,
            //    DeviceCharging: true,
            //    RunOnForeground: true,
            //    RequiredInternetAccess:InternetAccess.None
            //);

            //this._jobManager.Register(job);


            // Informar al usuario
            //await DisplayAlert("Información", "Trabajo en segundo plano iniciado", "OK");

            int uniId = unchecked((int)DateTime.Now.Ticks);

            Notification notification = new()
            {
                Channel = "alarms_notif",
                Id = unchecked((int)DateTime.Now.Ticks),
                Title = "Título de la notificación",
                Message = "Este es el cuerpo de la notificación",
                BadgeCount = 0,
                Thread = "Group",
                ScheduleDate = DateTime.Now.AddSeconds(2)
            };


              await _notificationManager.Send(notification);

            Task<IList<Notification>> nots = _notificationManager.GetPendingNotifications();


            var not =  await _notificationManager.GetNotification(uniId);

        }
    }
}

using Android.App;
using Android.Content;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.JSInterop;
using PayRemind.Contracts;
using PayRemind.Data;
using PayRemind.Jobs.MyJob;
using PayRemind.MauiWrapper;

//using PayRemind.Jobs.MyJob;
using PayRemind.Messages;
using PayRemind.Pages;
using Shiny;
using Shiny.Jobs;
//using Shiny.Notifications;
using Application = Microsoft.Maui.Controls.Application;
//using Notification = Shiny.Notifications.Notification;

namespace PayRemind
{
    public partial class MainPage : TabbedPage
    {
        private readonly AppTheme currentTheme = Application.Current == null ? AppTheme.Dark : Application.Current.RequestedTheme;


        private readonly IJobManager _jobManager;

        private bool _isInitialized = false;




        public MainPage( IJobManager jobManager)
        {
            InitializeComponent();


            _jobManager = jobManager;

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






            //Channel channel = new Channel
            //{
            //    Identifier = "alarms_notif",
            //    Description = "Channel",
            //    Importance = ChannelImportance.High,
            //    Actions = new List<ChannelAction>()
            //    {
            //        new ChannelAction()
            //        {
            //            Identifier = "alarm_btn",
            //            Title = "action_alarm",
            //            ActionType = ChannelActionType.OpenApp
            //        }
            //    }
            //};


            //_notificationManager.AddChannel(channel);

            if (!_isInitialized)
            {
                _isInitialized = true;
                Initialize();
            }


            //#if ANDROID
            //            if (OperatingSystem.IsAndroidVersionAtLeast(31)) // Android 12 y superior
            //            {
            //                var context = Android.App.Application.Context;
            //                var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
            //                if (alarmManager != null && !alarmManager.CanScheduleExactAlarms())
            //                {
            //                    // Necesitamos solicitar permiso al usuario
            //                    var intent = new Intent(Android.Provider.Settings.ActionRequestScheduleExactAlarm);
            //                    intent.AddFlags(ActivityFlags.NewTask);
            //                    context.StartActivity(intent);
            //                    return;
            //                }
            //            }



            //#endif



            Dispatcher.Dispatch(async () =>
            {

               await Task.Delay(5000);

                await Permissions.RequestAsync<Permissions.Reminders>();
                await Permissions.RequestAsync<Permissions.Battery>();
                await Permissions.RequestAsync<Permissions.PostNotifications>();
                await Permissions.RequestAsync<Permissions.Camera>();
                await Permissions.RequestAsync<Permissions.Flashlight>();

#if ANDROID
                Android.App.Activity? context = Platform.CurrentActivity;
                string? packageName = context?.PackageName ?? "";

                if (context?.GetSystemService(Context.PowerService) is PowerManager powerManager && !powerManager.IsIgnoringBatteryOptimizations(packageName))
                {
                    var intent = new Intent();
                    intent?.SetAction(Android.Provider.Settings.ActionRequestIgnoreBatteryOptimizations);
                    intent?.SetData(Android.Net.Uri.Parse($"package:{packageName}"));
                    context?.StartActivity(intent);
                }


                if (OperatingSystem.IsAndroidVersionAtLeast(31)) // Android 12 y superior
                {
                    if (context?.GetSystemService(Context.AlarmService) is AlarmManager alarmManager && !alarmManager.CanScheduleExactAlarms())
                    {
                        // Necesitamos solicitar permiso al usuario
                        var intent = new Intent(Android.Provider.Settings.ActionRequestScheduleExactAlarm);
                        intent.AddFlags(ActivityFlags.NewTask);
                        context?.StartActivity(intent);
                        await DisplayAlert("Permiso requerido", "Por favor, otorga permiso para programar alarmas exactas en la siguiente pantalla.", "OK");
                        return;
                    }
                }




#endif

            });


            



            //FabButton.SizeChanged += OnButtonSizeChanged;



        }


        private void OnButtonSizeChanged(object sender, EventArgs e)
        {

            if (FabButton.Width > 0 && FabButton.Height > 0)
            {
                Dispatcher.Dispatch(async () =>
                {
                    if (!FabButton.IsVisible) return;

                    await ShowFeatureHighlight();


                    //_myTooltip.ShowAt(MyButton, offsetY: 10, offsetX: 0);
                });

                // Desuscribirse del evento si no es necesario más
                FabButton.SizeChanged -= OnButtonSizeChanged;
            }
        }


        private async Task ShowFeatureHighlight(Button btn = null)
        {

            App.TutorialDone = true;

            // Asegúrate de que estás ejecutando esto en el hilo de la UI

            // Necesitamos la vista de Android para pasársela al servicio

#if ANDROID
            // Asegúrate de que el botón esté completamente cargado
            //var platformButton = MyButton.ToPlatform(this.Handler.MauiContext);

            //var activity = Platform.CurrentActivity;
            //Microsoft.Maui.Controls.View nativeView = MyButton;

            // showCaseService.ShowFeatureHighlight(nativeView.Id, "Guide Title", "Guide Description");

            await Task.Delay(100);

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {

                var viewConverterService = DependencyService.Get<IViewConverterService>();

                Android.Views.View nativeView = viewConverterService.GetNativeView(FabButton);

                if (nativeView != null)
                {
                    ShowCaseViewWrapper.ShowGuideView(nativeView, "Presiona este botón para crear un recordatorio", "Este botón abrira un formulario");
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


        private async void FabButton_Clicked(object sender, EventArgs e)
        {
            //Dispatcher.Dispatch(async () =>
            //{
            //    await OnSendNotificationClicked();
            //});


            if (!App.TutorialDone)
            {
                //await ShowFeatureHighlight();


                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    AccessState accessState = await _jobManager.RequestAccess();
                    if (accessState != AccessState.Available)
                    {

                        await DisplayAlert("Alerta", "No se tienen los permisos necesarios para ejecutar jobs en segundo plano.", "OK");


                        return;
                    }


                    var jobInfo = new JobInfo(
                                             JobType: typeof(MyBackgroundJob),
                                             Identifier: "notificationsTwo",  // Identificador único para el job
                                             RequiredInternetAccess: InternetAccess.None,  // Acceso a internet requerido
                                             BatteryNotLow: false,                  // Ejecutar incluso con batería baja
                                             DeviceCharging: false,                 // Ejecutar incluso si no está cargando
                                             RunOnForeground: true                  // Ejecutar en primer plano,
                                             , IsSystemJob: false
                                         );

                    try
                    {
                        _jobManager.Cancel("notificationsTwo");


                        _jobManager.Register(jobInfo);

                        var results = await _jobManager.RunAll();
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Job {result.Job?.Identifier} ejecutado. Éxito: {result.Success}");
                        }

                        await DisplayAlert("Éxito", "Job programado correctamente", "OK");

                        App.TutorialDone = true;

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"No se pudo programar el job: {ex.Message}", "OK");
                        App.TutorialDone = true;
                    }

                });


            }
            else
            {

                WeakReferenceMessenger.Default.Send(new OpenDialog(true));


        


            }



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
            //Shiny.AccessState result = await _notificationManager.RequestAccess(AccessRequestFlags.Notification);




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

            //int uniId = unchecked((int)DateTime.Now.Ticks);

            //Notification notification = new()
            //{
            //    Channel = "alarms_notif",
            //    Id = unchecked((int)DateTime.Now.Ticks),
            //    Title = "Título de la notificación",
            //    Message = "Este es el cuerpo de la notificación",
            //    BadgeCount = 0,
            //    Thread = "Group",
            //    ScheduleDate = DateTime.Now.AddSeconds(2)
            //};


            //  await _notificationManager.Send(notification);

            //Task<IList<Notification>> nots = _notificationManager.GetPendingNotifications();


            //var not =  await _notificationManager.GetNotification(uniId);

        }
    }
}

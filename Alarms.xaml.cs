using Android.App;
using Android.Content;
using PayRemind.Contracts;

namespace PayRemind;

public partial class Alarms : ContentPage
{


    public Alarms()
	{
		InitializeComponent();
    }

    private async void OnSetAlarmClicked(object sender, EventArgs e)
    {

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
                await DisplayAlert("Permiso requerido", "Por favor, otorga permiso para programar alarmas exactas en la siguiente pantalla.", "OK");
                return;
            }
        }
#endif


        IAlarmService _alarmService = DependencyService.Get<IAlarmService>();


        var alarmTime = DateTime.Now.AddMinutes(1); // Ejemplo: alarma en 5 minutos
        _alarmService.SetAlarm(alarmTime, "Em bimcho");
        DisplayAlert("Alarma configurada", $"La alarma se activará a las {alarmTime.ToString("HH:mm")}", "OK");
    }
}
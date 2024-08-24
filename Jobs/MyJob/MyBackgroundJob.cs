using Android.App.Job;
using Xamarin.KotlinX.Coroutines;
using Shiny;
using Shiny.Jobs;
using IJob = Shiny.Jobs.IJob;

namespace PayRemind.Jobs.MyJob
{
    public class MyBackgroundJob : IJob
    {
        public async Task Run(Shiny.Jobs.JobInfo jobInfo, CancellationToken cancelToken)
        {
              // Aquí va la lógica de tu tarea en segundo plano
            while (!cancelToken.IsCancellationRequested)
            {
                // Ejemplo: registrar la hora actual cada 5 minutos
                Console.WriteLine($"Tarea en segundo plano ejecutada a las {DateTime.Now}");
                await Task.Delay(TimeSpan.FromMinutes(5), cancelToken);
            }
        }
    }
}

using PayRemind.Pages.Custom;
using PayRemind.Wrappers;

namespace PayRemind.Pages;

public partial class Show : ContentPage
{
	public Show()
	{
		InitializeComponent();

  
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


        Button1.Loaded += (s, e) =>
        {

            var showcase1 = new ShowcaseView(Button1, "Este es el primer botón");
            var showcase2 = new ShowcaseView(Button2, "Este es el segundo botón");

            //var sequence = new ShowcaseSequence();
            //sequence.AddShowcase(showcase1)
            //        .AddShowcase(showcase2)
            //.Start();

            showcase1.Show();

            //var sequence = new ShowcaseSequence();
            //sequence.AddShowcase(showcase1)
            //.Start();

            showcase1.Dismissed += (s, args) =>
            {
                // Acción después de que se cierre el ShowcaseView
                DisplayAlert("Showcase", "El ShowcaseView se ha cerrado", "OK");
            };

            showcase2.Dismissed += (s, args) =>
            {
                // Acción después de que se cierre el ShowcaseView
                DisplayAlert("Showcase", "El ShowcaseView se ha cerrado", "OK");
            };
        };



    }
}
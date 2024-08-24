using PayRemind.Pages.Custom;

namespace PayRemind.Pages;

public partial class Show : ContentPage
{
	public Show()
	{
		InitializeComponent();

  
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var showcase1 = new ShowcaseView(Button1, "Este es el primer botón");
        var showcase2 = new ShowcaseView(Button2, "Este es el segundo botón");

        var sequence = new ShowcaseSequence();
        sequence.AddShowcase(showcase1)
                .AddShowcase(showcase2)
                .Start();
    }
}
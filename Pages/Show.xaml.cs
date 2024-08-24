using PayRemind.Pages.Custom;

namespace PayRemind.Pages;

public partial class Show : ContentPage
{
    public Show()
    {
        InitializeComponent();
        InitializeShowcase();
    }

    private void InitializeShowcase()
    {
        ShowcaseView.AddShowcaseItem(Button1, "Este es el primer botón");
        ShowcaseView.AddShowcaseItem(Button2, "Este es el segundo botón");

        Button1.Clicked += OnShowShowcase;
    }

    private async void OnShowShowcase(object sender, EventArgs e)
    {
        await ShowcaseView.Show();
    }
}
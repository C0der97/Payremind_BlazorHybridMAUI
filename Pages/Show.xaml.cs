using PayRemind.Pages.Custom;
using PayRemind.Wrappers;

namespace PayRemind.Pages;

public partial class Show : ContentPage
{
    private ShowcaseView _showcaseView;

    public Show()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_showcaseView == null)
        {
            InitializeShowcase();
        }
    }

    private void InitializeShowcase()
    {
        _showcaseView = new ShowcaseView();
        _showcaseView.AddShowcaseItem(Button1, "Este es el primer botón");
        _showcaseView.AddShowcaseItem(Button2, "Este es el segundo botón");

        // Añadir ShowcaseView a la página
        Content = new Grid
        {
            Children =
            {
                new VerticalStackLayout
                {
                    Children = { Button1, Button2 },
                    VerticalOptions = LayoutOptions.Center
                },
                _showcaseView
            }
        };

        // Ocultar inicialmente el ShowcaseView
        _showcaseView.IsVisible = false;

        // Añadir un evento para mostrar el ShowcaseView
        Button1.Clicked += OnShowShowcase;
    }

    private async void OnShowShowcase(object sender, EventArgs e)
    {
        await _showcaseView.Show();
    }
}
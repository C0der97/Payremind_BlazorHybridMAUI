namespace PayRemind.Pages;
using Microsoft.Maui.Controls;

public partial class Guided : ContentPage
{

    private CustomTooltip _myTooltip;


    public Guided()
	{
        InitializeComponent();
        _myTooltip = new CustomTooltip
        {
            Title = "Tap to See Menu Options",
            Text = "This button opens the menu",
            IsVisible = false,
        };

        MyButton.Clicked += ToggleTooltip;
    }


    private void ToggleTooltip(object sender, EventArgs e)
    {
        if (_myTooltip.IsVisible)
        {
            _myTooltip.IsVisible = false;
        }
        else
        {
            _myTooltip.ShowAt(MyButton, offsetY: 10);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Mostrar el tooltip automáticamente cuando la página aparezca
        //_myTooltip.ShowAt(MyButton, offsetY: 10);

        MyButton.SizeChanged += OnButtonSizeChanged;
    }

    private void OnButtonSizeChanged(object sender, EventArgs e)
    {
        if (MyButton.Width > 0 && MyButton.Height > 0)
        {
            Dispatcher.Dispatch(() =>
            {
                if (!MyButton.IsVisible) return;

                //_myTooltip.ShowAt(MyButton, offsetY: 10, offsetX: 0);
            });

            // Desuscribirse del evento si no es necesario más
            MyButton.SizeChanged -= OnButtonSizeChanged;
        }
    }

}
namespace PayRemind.Pages;
using Microsoft.Maui.Controls;
using PayRemind.Pages.Custom;

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
            IsVisible = false
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

}
namespace PayRemind.Pages;
using Microsoft.Maui.Controls;
using PayRemind.Pages.Custom;

public partial class Guided : ContentPage
{




    public Guided()
	{
        InitializeComponent();
        SetupTooltip();
    }


    private TooltipCustom tooltip;


    private void SetupTooltip()
    {
        tooltip = new TooltipCustom();
        absoluteLayout.Children.Add(tooltip);
    }

    private void OnShowTooltipClicked(object sender, EventArgs e)
    {
        tooltip.ShowTooltip((View)sender, "This is a tooltip!");
    }

    private void OnHideTooltipClicked(object sender, EventArgs e)
    {
        tooltip.HideTooltip();
    }


    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    this.Loaded += (s, e) =>
    //    {
    //        // Espera un momento para asegurar que la UI está completamente cargada
    //        Dispatcher.Dispatch(() => SetupGuide());
    //    };
    //}

    private void OnStartGuideClicked(object sender, EventArgs e)
    {
        DisplayAlert("Guide Started", "The guide would start here.", "OK");

    }
}
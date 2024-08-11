namespace PayRemind.Pages;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using PayRemind.Contracts;
using PayRemind.MauiWrapper;
using PayRemind.Platforms.Android;
using PayRemind.Platforms.Android.Wrappers;

public partial class Guided : ContentPage
{

    private CustomTooltip? _myTooltip;

    private readonly IViewConverterService? _viewConverterService;

    public Guided()
    {
        InitializeComponent();
    }

    public Guided(IViewConverterService viewConverterService)
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

    private async Task ShowFeatureHighlight(Button btn = null)
    {
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

            Android.Views.View nativeView = viewConverterService.GetNativeView(MyButton);
            Android.Views.View nativeView2 = viewConverterService.GetNativeView(MyButtonTwo);

            if (nativeView != null)
                {
                    ShowCaseViewWrapper.ShowGuideView(nativeView, nativeView2, "Welcome!", "This is a sample button. Tap it to perform an action.");
                }

            }

#endif
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
            Dispatcher.Dispatch(async () =>
            {
                if (!MyButton.IsVisible) return;

               await ShowFeatureHighlight();


                //_myTooltip.ShowAt(MyButton, offsetY: 10, offsetX: 0);
            });

            // Desuscribirse del evento si no es necesario más
            MyButton.SizeChanged -= OnButtonSizeChanged;
        }
    }

    private async void MyButton_Clicked(object sender, EventArgs e)
    {
       await ShowFeatureHighlight();
    }
}
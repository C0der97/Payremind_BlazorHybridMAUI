using Microsoft.Maui.Layouts;
using PayRemind.Pages.Custom;
using SkiaSharp.Views.Maui;

namespace PayRemind.Pages;

public partial class CustomTooltip : ContentView
{
    public static readonly BindableProperty TitleProperty =
               BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomTooltip), string.Empty);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomTooltip), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public CustomTooltip()
    {
        InitializeComponent();
        TooltipTitle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        TooltipText.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));

        var tapGestureRecognizer = new TapGestureRecognizer();
    tapGestureRecognizer.Tapped += (s, e) => 
    {
        this.IsVisible = false;
    };
    this.GestureRecognizers.Add(tapGestureRecognizer);

        DrawArrow();

    }

    public async void ShowAt(View targetView, double offsetX = 0, double offsetY = 0)
    {
        if (targetView.Parent is not Layout layout)
        {
            return;
        }

        // Calcula la posici�n del tooltip relativa al targetView
        double targetX = targetView.X + offsetX;
        double targetY = targetView.Y + targetView.Height + offsetY;

        // Ajusta la posici�n del tooltip
        this.TranslationX = targetX;
        this.TranslationY = targetY;

        // Aseg�rate de que el tooltip tenga un tama�o
        this.WidthRequest = -1; // O el ancho que prefieras
        this.HeightRequest = -1; // Altura autom�tica

        this.Opacity = 0;
        this.IsVisible = true;

        if (!layout.Children.Contains(this))
        {
            layout.Children.Add(this);
        }

        await Task.Delay(10); // Espera a que se actualice el layout
        AdjustPosition(targetView);
        await this.FadeTo(1, 250);
    }

    private void AdjustPosition(View targetView)
    {
        // Centra el tooltip horizontalmente con respecto al bot�n
        this.TranslationX = targetView.X + (targetView.Width - this.Width) / 2;

        // Aseg�rate de que el tooltip est� completamente debajo del bot�n
        this.TranslationY = targetView.Y + 10; // Agrega un peque�o espacio

    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        // Este m�todo se llamar� cada vez que el tama�o del tooltip cambie
        //AdjustPosition();
    }




    private void DrawArrow()
    {
        ArrowGraphicsView.Drawable = new ArrowDrawable();
    }


}
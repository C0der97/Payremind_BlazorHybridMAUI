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



        double targetX = targetView.X;
        double targetY = targetView.Y + targetView.Height;

        this.TranslationX = (targetX / 2 ) - 40;
        this.TranslationY = targetY + offsetY;

        this.Opacity = 0;
        this.IsVisible = true;

        await this.FadeTo(1, 250); // Desvanece en 250 milisegundos

        if (!layout.Children.Contains(this))
        {
            layout.Children.Add(this);
        }
    }



    private void DrawArrow()
    {
        ArrowGraphicsView.Drawable = new ArrowDrawable();
    }


}
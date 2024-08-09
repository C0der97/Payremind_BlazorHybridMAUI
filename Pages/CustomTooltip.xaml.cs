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

        // Asegúrate de que el layout es un AbsoluteLayout
        AbsoluteLayout absoluteLayout;
        if (layout is not AbsoluteLayout)
        {
            absoluteLayout = new AbsoluteLayout();
            layout.Children.Clear();
            layout.Children.Add(absoluteLayout);
        }
        else
        {
            absoluteLayout = (AbsoluteLayout)layout;
        }

        double targetX = targetView.X + offsetX;
        double targetY = targetView.Y + targetView.Height + offsetY;

        AbsoluteLayout.SetLayoutBounds(this, new Rect(targetX + offsetX, targetY + offsetY, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
        AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.None);

        this.Opacity = 0;
        this.IsVisible = true;

        if (!absoluteLayout.Children.Contains(this))
        {
            absoluteLayout.Children.Add(this);
        }

        await Task.Delay(10);

        AdjustPosition();

        await this.FadeTo(1, 250);
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        // Este método se llamará cada vez que el tamaño del tooltip cambie
        AdjustPosition();
    }

    private void AdjustPosition()
    {
        // Ajusta la posición vertical del tooltip
        // Asumiendo que quieres que aparezca encima del elemento objetivo
        //this.TranslationY -= this.Height + 40;

        // Si quieres ajustar también la posición horizontal, puedes hacerlo aquí
        // Por ejemplo, para centrar el tooltip:
        // this.TranslationX = (targetView.Width - this.Width) / 2;
    }



    private void DrawArrow()
    {
        ArrowGraphicsView.Drawable = new ArrowDrawable();
    }


}
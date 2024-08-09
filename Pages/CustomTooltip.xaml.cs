using Microsoft.Maui.Layouts;

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
    }

    public void ShowAt(View targetView, double offsetX = 0, double offsetY = 0)
    {
        if (targetView.Parent is not Layout layout)
        {
            return;
        }

        // Asegúrate de que el tooltip no está ya en el layout
        if (!layout.Children.Contains(this))
        {
            layout.Add(this);
        }

        // Posiciona el tooltip
        this.TranslationX = targetView.X + offsetX;
        this.TranslationY = targetView.Y + targetView.Height + offsetY;

        // Ajusta la posición si el tooltip se sale de los límites
        this.SizeChanged += (s, e) =>
        {
            if (this.TranslationX + this.Width > layout.Width)
            {
                this.TranslationX = layout.Width - this.Width;
            }
            if (this.TranslationY + this.Height > layout.Height)
            {
                this.TranslationY = targetView.Y - this.Height - offsetY;
            }
        };

        this.IsVisible = true;
    }
}
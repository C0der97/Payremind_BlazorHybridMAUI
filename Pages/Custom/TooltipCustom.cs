namespace PayRemind.Pages.Custom
{
    public class TooltipCustom : Frame
    {
        public static readonly BindableProperty TooltipTextProperty =
           BindableProperty.Create(nameof(TooltipText), typeof(string), typeof(TooltipCustom), string.Empty);

        public string TooltipText
        {
            get => (string)GetValue(TooltipTextProperty);
            set => SetValue(TooltipTextProperty, value);
        }

        public TooltipCustom()
        {
            BackgroundColor = Colors.Black;
            Padding = new Thickness(10);
            HasShadow = true;
            CornerRadius = 5;
            IsVisible = false;

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        TextColor = Colors.White,
                        FontSize = 14,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            };

            ((Label)((StackLayout)Content).Children[0]).SetBinding(Label.TextProperty, new Binding(nameof(TooltipText), source: this));
        }

        public void ShowTooltip(View targetView, string text)
        {
            TooltipText = text;
            IsVisible = true;

            var absoluteLayout = targetView.Parent as AbsoluteLayout;
            if (absoluteLayout == null)
            {
                throw new InvalidOperationException("Target view must be inside an AbsoluteLayout");
            }

            var targetBounds = targetView.Bounds;
            AbsoluteLayout.SetLayoutBounds(this, new Rect(
                targetBounds.Top,
                targetBounds.Bottom,
                AbsoluteLayout.AutoSize,
                AbsoluteLayout.AutoSize));
        }

        public void HideTooltip()
        {
            IsVisible = false;
        }
    }
}

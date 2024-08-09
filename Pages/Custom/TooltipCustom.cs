namespace PayRemind.Pages.Custom
{
    public class TooltipCustom : AbsoluteLayout
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
            BackgroundColor = Colors.Transparent;
            Padding = new Thickness(10);
            IsVisible = false;

            // Create tooltip content
            var tooltipContent = new StackLayout
            {
                Padding = new Thickness(10),
                BackgroundColor = Colors.Black,
                Children =
                {
                    new Label
                    {
                        TextColor = Colors.White,
                        FontSize = 14,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center
                    }
                },
                HorizontalOptions = LayoutOptions.Start,
            };

            ((Label)tooltipContent.Children[0]).SetBinding(Label.TextProperty, new Binding(nameof(TooltipText), source: this));

            // Create tooltip arrow
            var arrow = new BoxView
            {
                Color = Colors.Red,
                WidthRequest = 10,
                HeightRequest = 10,
                Rotation = 45
            };

            // Arrange the layout
            Children.Add(arrow);
            Children.Add(tooltipContent);
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

            // Calculate tooltip position
            var tooltipPosition = new Point(
                targetBounds.Left + (targetBounds.Width / 2),
                targetBounds.Bottom - HeightRequest);

            // Set tooltip layout bounds
            AbsoluteLayout.SetLayoutBounds(this, new Rect(
                tooltipPosition.X,
                tooltipPosition.Y,
                WidthRequest,
                HeightRequest));

            // Position the arrow
            var arrow = Children.FirstOrDefault(c => c is BoxView) as BoxView;
            if (arrow != null)
            {
                var arrowPosition = new Point(
                    WidthRequest / 2 - arrow.WidthRequest / 2,
                    -arrow.HeightRequest);

                AbsoluteLayout.SetLayoutBounds(arrow, new Rect(
                    arrowPosition.X,
                    arrowPosition.Y,
                    arrow.WidthRequest,
                    arrow.HeightRequest));
            }
        }


        public void HideTooltip()
        {
            IsVisible = false;
        }
    }
}

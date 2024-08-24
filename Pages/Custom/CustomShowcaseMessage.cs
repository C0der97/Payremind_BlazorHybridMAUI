namespace PayRemind.Pages.Custom
{
    public class CustomShowcaseMessage : ContentView
    {
        private Label _label;

        public CustomShowcaseMessage()
        {
            BackgroundColor = Colors.Black.WithAlpha(0.7f);
            Padding = new Thickness(16, 8);

            _label = new Label
            {
                TextColor = Colors.White,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Content = new Frame
            {
                BackgroundColor = Colors.Transparent,
                BorderColor = Colors.Transparent,
                CornerRadius = 8,
                Padding = 0,
                Content = _label
            };
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomShowcaseMessage), propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomShowcaseMessage)bindable;
            control._label.Text = (string)newValue;
            System.Diagnostics.Debug.WriteLine($"Text changed to: {newValue}"); // Log para debug
        }
    }
}

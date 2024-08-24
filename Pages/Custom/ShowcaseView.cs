using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;

namespace PayRemind.Pages.Custom
{
    public class ShowcaseView : ContentView
    {
        private AbsoluteLayout _layout;
        private BoxView _backgroundBox;
        private BoxView _highlightBox;
        private Label _messageLabel;
        private ObservableCollection<ShowcaseItem> _showcaseItems;
        private int _currentIndex = 0;

        public event EventHandler Dismissed;

        public ShowcaseView()
        {
            _showcaseItems = new ObservableCollection<ShowcaseItem>();
            SetupShowcase();
        }

        public void AddShowcaseItem(View targetView, string message)
        {
            _showcaseItems.Add(new ShowcaseItem(targetView, message));
        }

        private void SetupShowcase()
        {
            _layout = new AbsoluteLayout();

            _backgroundBox = new BoxView
            {
                Color = Colors.Black.WithAlpha(0.1f),
                InputTransparent = true
            };
            AbsoluteLayout.SetLayoutBounds(_backgroundBox, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_backgroundBox, AbsoluteLayoutFlags.All);

            _highlightBox = new BoxView
            {
                Color = Colors.Yellow.WithAlpha(0.4f),
                InputTransparent = true
            };

            _messageLabel = new Label
            {
                TextColor = Colors.White,
                BackgroundColor = Colors.Black,
                Padding = new Thickness(10),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            };

            _layout.Children.Add(_backgroundBox);
            _layout.Children.Add(_highlightBox);
            _layout.Children.Add(_messageLabel);

            Content = _layout;

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += OnTapped;
            _layout.GestureRecognizers.Add(tapGesture);
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            if (_currentIndex < _showcaseItems.Count - 1)
            {
                _currentIndex++;
                await ShowCurrentItem();
            }
            else
            {
                Dismiss();
            }
        }

        public async Task Show()
        {
            if (_showcaseItems.Count == 0) return;

            this.IsVisible = true;
            _currentIndex = 0;
            await ShowCurrentItem();
        }

        private async Task ShowCurrentItem()
        {
            var item = _showcaseItems[_currentIndex];
            var targetPosition = GetAbsolutePosition(item.TargetView);

            _messageLabel.Text = item.Message;

            await Task.WhenAll(
                _highlightBox.FadeTo(0, 150),
                _messageLabel.FadeTo(0, 150)
            );

            AbsoluteLayout.SetLayoutBounds(_highlightBox, new Rect(targetPosition.X, targetPosition.Y, item.TargetView.Width, item.TargetView.Height));
            _highlightBox.CornerRadius = (float)Math.Min(item.TargetView.Width, item.TargetView.Height) / 2;

            // Ajustar la posición del mensaje para que sea visible
            double messageY = targetPosition.Y - 40; // Coloca el mensaje encima del botón
            if (messageY < 0)
            {
                messageY = targetPosition.Y + item.TargetView.Height + 10; // Si no cabe arriba, colócalo debajo
            }

            AbsoluteLayout.SetLayoutBounds(_messageLabel, new Rect(10, messageY, this.Width - 20, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(_messageLabel, AbsoluteLayoutFlags.WidthProportional);

            // Asegúrate de que el mensaje tenga un fondo para mayor visibilidad
            _messageLabel.BackgroundColor = Colors.Black.WithAlpha(0.7f);
            _messageLabel.TextColor = Colors.White;
            _messageLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            _messageLabel.HorizontalTextAlignment = TextAlignment.Center;
            _messageLabel.Padding = new Thickness(10);

            await Task.WhenAll(
                _highlightBox.FadeTo(1, 300),
                _messageLabel.FadeTo(1, 300)
            );
        }
        public void Dismiss()
        {
            this.IsVisible = false;
            Dismissed?.Invoke(this, EventArgs.Empty);
        }

        private Point GetAbsolutePosition(View view)
        {
            var element = view;
            var x = element.X;
            var y = element.Y;

            while (element.Parent != null)
            {
                if (element.Parent is View parentView)
                {
                    x += parentView.X;
                    y += parentView.Y;
                    element = parentView;
                }
                else
                {
                    break;
                }
            }

            return new Point(x, y);
        }
    }

    public class ShowcaseItem
    {
        public View TargetView { get; set; }
        public string Message { get; set; }

        public ShowcaseItem(View targetView, string message)
        {
            TargetView = targetView;
            Message = message;
        }
    }
}
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;

namespace PayRemind.Pages.Custom
{
    public class ShowcaseView : ContentView
    {
        private AbsoluteLayout _layout;
        private BoxView _backgroundBox;
        private BoxView _highlightBox;
        private CustomShowcaseMessage _messageView;
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

            _messageView = new CustomShowcaseMessage
            {
                IsVisible = true, // Asegurarse de que sea visible
            };

            _layout.Children.Add(_backgroundBox);
            _layout.Children.Add(_highlightBox);
            _layout.Children.Add(_messageView);

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

            System.Diagnostics.Debug.WriteLine($"Showing message: {item.Message}"); // Log para debug

            _messageView.Text = item.Message;
            _messageView.IsVisible = true; // Asegurarse de que sea visible

            await Task.WhenAll(
                _highlightBox.FadeTo(0, 150),
                _messageView.FadeTo(0, 150)
            );

            AbsoluteLayout.SetLayoutBounds(_highlightBox, new Rect(targetPosition.X, targetPosition.Y, item.TargetView.Width, item.TargetView.Height));
            _highlightBox.CornerRadius = (float)Math.Min(item.TargetView.Width, item.TargetView.Height) / 2;

            // Posicionar el mensaje
            double messageY = targetPosition.Y + item.TargetView.Height + 10;
            if (messageY + 50 > this.Height) // Si el mensaje se sale de la pantalla
            {
                messageY = targetPosition.Y - 60; // Colocar el mensaje arriba del elemento
            }

            AbsoluteLayout.SetLayoutBounds(_messageView, new Rect(10, messageY, this.Width - 20, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(_messageView, AbsoluteLayoutFlags.WidthProportional);

            await Task.WhenAll(
                _highlightBox.FadeTo(1, 300),
                _messageView.FadeTo(1, 300)
            );

            System.Diagnostics.Debug.WriteLine($"Message view bounds: {AbsoluteLayout.GetLayoutBounds(_messageView)}"); // Log para debug
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
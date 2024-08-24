using Microsoft.Maui.Layouts;

namespace PayRemind.Pages.Custom
{
    public class ShowcaseView : ContentView
    {
        public View TargetView { get; set; }
        public string Message { get; set; }
        public Color HighlightColor { get; set; } = Colors.Yellow;

        public event EventHandler Dismissed;

        public ShowcaseView(View targetView, string message)
        {
            TargetView = targetView;
            Message = message;

            // Configuración de la vista
            SetupShowcase();

            // Añadir un gesto de toque para cerrar
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => Dismiss();
            this.GestureRecognizers.Add(tapGesture);
        }

        private void SetupShowcase()
        {
            var layout = new AbsoluteLayout();

            // Crear el fondo transparente
            var backgroundBox = new BoxView
            {
                Color = Colors.Black.MultiplyAlpha(0.5f), // Fondo semi-transparente
                WidthRequest = Application.Current.MainPage.Width,
                HeightRequest = Application.Current.MainPage.Height
            };
            AbsoluteLayout.SetLayoutBounds(backgroundBox, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(backgroundBox, AbsoluteLayoutFlags.All);

            // Crear el BoxView para resaltar el TargetView
            var highlightBox = new BoxView
            {
                Color = HighlightColor,
                CornerRadius = (float)TargetView.Width / 2,
                WidthRequest = TargetView.Width,
                HeightRequest = TargetView.Height,
                Opacity = 0.3
            };
            AbsoluteLayout.SetLayoutBounds(highlightBox, new Rect(TargetView.X, TargetView.Y, TargetView.Width, TargetView.Height));
            AbsoluteLayout.SetLayoutFlags(highlightBox, AbsoluteLayoutFlags.None);

            // Crear el Label para mostrar el mensaje
            var messageLabel = new Label
            {
                Text = Message,
                TextColor = Colors.White,
                BackgroundColor = Colors.Black,
                Padding = new Thickness(10),
                WidthRequest = 200,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };
            AbsoluteLayout.SetLayoutBounds(messageLabel, new Rect(TargetView.X + 10, TargetView.Y + TargetView.Height + 20, 200, 50));
            AbsoluteLayout.SetLayoutFlags(messageLabel, AbsoluteLayoutFlags.None);

            // Añadir los elementos al AbsoluteLayout
            layout.Children.Add(backgroundBox);
            layout.Children.Add(highlightBox);
            layout.Children.Add(messageLabel);

            // Asignar el layout al Content de la ContentView
            this.Content = layout;
        }

        public void Show()
        {
            // Mostrar la vista
            Application.Current.MainPage?.Navigation.PushModalAsync(new ContentPage { Content = this, BackgroundColor = Colors.Transparent });
        }

        public void Dismiss()
        {
            // Ocultar la vista
            Application.Current.MainPage?.Navigation.PopModalAsync();

            // Disparar el evento Dismissed cuando se oculta la vista
            Dismissed?.Invoke(this, EventArgs.Empty);
        }
    }

}

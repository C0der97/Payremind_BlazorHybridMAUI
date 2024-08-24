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

            // Obtener las coordenadas absolutas del TargetView
            var targetPosition = GetAbsolutePosition(TargetView);

            // Crear el fondo transparente
            var backgroundBox = new BoxView
            {
                Color = Colors.Transparent, // Fondo semi-transparente
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
            AbsoluteLayout.SetLayoutBounds(highlightBox, new Rect(targetPosition.X, targetPosition.Y, TargetView.Width, TargetView.Height));
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
            AbsoluteLayout.SetLayoutBounds(messageLabel, new Rect(targetPosition.X + 10, targetPosition.Y + TargetView.Height + 20, 200, 50));
            AbsoluteLayout.SetLayoutFlags(messageLabel, AbsoluteLayoutFlags.None);

            // Añadir los elementos al AbsoluteLayout
            layout.Children.Add(backgroundBox);
            layout.Children.Add(highlightBox);
            layout.Children.Add(messageLabel);

            // Asignar el layout al Content de la ContentView
            this.Content = layout;
        }

        private Point GetAbsolutePosition(View view)
        {
            var element = view;
            var x = element.X;
            var y = element.Y;

            while (element.Parent is VisualElement parent)
            {
                x += parent.X;
                y += parent.Y;

                // Solo continuar si el padre puede ser convertido a View
                if (parent is View parentView)
                {
                    element = parentView; // Usamos el cast seguro
                }
                else
                {
                    break; // Si no es un View, salimos del bucle
                }
            }

            return new Point(x, y);
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

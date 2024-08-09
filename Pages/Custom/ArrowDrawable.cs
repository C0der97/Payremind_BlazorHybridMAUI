namespace PayRemind.Pages.Custom
{
    public class ArrowDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Configura el color y el grosor del trazo
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;

            // Dibuja la flecha
            var arrowPath = new PathF();
            arrowPath.MoveTo(10, 0); // Punto inicial (cima)
            arrowPath.LineTo(20, 10); // Línea hacia abajo derecha
            arrowPath.LineTo(0, 10); // Línea hacia abajo izquierda
            arrowPath.Close(); // Cierra el camino

            canvas.DrawPath(arrowPath);
        }
    }
}

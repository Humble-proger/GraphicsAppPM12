using Avalonia.Controls;
using Avalonia.Media;

public class Point : Avalonia.Controls.Shapes.Ellipse
{
    public Point(double x, double y, double thickness = 1, string color = "#000000")
    {
        Width = thickness;
        Height = thickness;
        Fill = new SolidColorBrush(Color.Parse(color));
        Canvas.SetLeft(this, x);
        Canvas.SetTop(this, y);
    }
}

